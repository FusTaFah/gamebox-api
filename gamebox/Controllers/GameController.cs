﻿using GameBox.IGDBResponse;
using GameBox.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace GameBox.Controllers
{
    /// <summary>
    /// Game information service
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    public class GameController : Controller
    {
        /// <summary>
        /// Retrieves a game from the IGDB by name
        /// </summary>
        /// <param name="gameTitle">The title of the game to search for</param>
        /// <returns></returns>
        [HttpGet(Name = "games/search")]
        public async Task<GameBox.Models.Game?> Get(string gameTitle)
        {
            var client = new HttpClient();
            var tokenRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://id.twitch.tv/oauth2/token"),
                Content = new StringContent("{" + Environment.NewLine +
                    "\"client_id\": \"gkerplqbddwqjd5s253q471ztu2pth\"," + Environment.NewLine +
                    "\"client_secret\": \"lsbgel1hnihcwnwh3a1y6rw9zmqvup\"," + Environment.NewLine +
                    "\"grant_type\": \"client_credentials\"" + Environment.NewLine + "}")
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };

            Token? tokenResp = null;
            using (var tokenResponse = await client.SendAsync(tokenRequest))
            {
                tokenResponse.EnsureSuccessStatusCode();
                string tokenRespString = await tokenResponse.Content.ReadAsStringAsync();
                tokenResp = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(tokenRespString);
                if (tokenResp == null)
                    return null;
            }

            var gameRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.igdb.com/v4/games"),
                Content = new StringContent($"fields name,external_games,summary,cover;\r\nwhere name = \"{gameTitle}\";")
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue ("text/plain")
                    }
                },
                Headers =
                {
                    { "Client-ID", "gkerplqbddwqjd5s253q471ztu2pth" },
                    { "Authorization", "Bearer " + tokenResp.access_token }
                },
            };

            GameBox.IGDBResponse.Game? gameResp = null;
            using (var gameResponse = await client.SendAsync(gameRequest))
            {
                gameResponse.EnsureSuccessStatusCode();
                string gameRespString = await gameResponse.Content.ReadAsStringAsync();
                gameResp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GameBox.IGDBResponse.Game>>(gameRespString)?.FirstOrDefault();
                if (gameResp == null)
                    return null;
            }

            var coverRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.igdb.com/v4/covers"),
                Content = new StringContent($"fields url;\r\nwhere game = {gameResp.id};")
                {
                    Headers =
                    {
                        ContentType = MediaTypeHeaderValue.Parse ("text/plain")
                    }
                },
                Headers =
                {
                    { "Client-ID", "gkerplqbddwqjd5s253q471ztu2pth" },
                    { "Authorization", "Bearer " + tokenResp.access_token }
                }
            };

            GameBox.IGDBResponse.Cover? coverResp;
            using (var coverResponse = await client.SendAsync(coverRequest))
            {
                string coverRespString = await coverResponse.Content.ReadAsStringAsync();
                coverResp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GameBox.IGDBResponse.Cover>>(coverRespString)?.FirstOrDefault();
                if (coverResp == null)
                    return null;
            }

            return new GameBox.Models.Game()
            {
                External_ID = gameResp.external_games,
                Title = gameResp.name,
                Description = gameResp.summary,
                ImagePath = coverResp.url
            };
        }
    }
}
