using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

namespace Score
{
    public class LeaderboardManeger : MonoBehaviour
    {
        const string LEADERBOARD_ID = "Best_Score_leaderboard";
        [SerializeField] ScoreCalculator score;
        [SerializeField] LeaderboardShower shower;

        private async void Awake()
        {
            try
            {
                await UnityServices.InitializeAsync();
                if (AuthenticationService.Instance.IsSignedIn) return;
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void AddScore()
        {
            if (score == null) throw new System.NullReferenceException("ScoreCalculator");
            AddScoreOnBoard(score.score);
        }

        public async void AddScoreOnBoard(double data)
        {
            if (AuthenticationService.Instance.AccessToken == null) return;
            var result = await LeaderboardsService.Instance.AddPlayerScoreAsync(LEADERBOARD_ID, data);
            Debug.Log(JsonConvert.SerializeObject(result));
            shower.Show();
        }

        public async UniTask<LeaderboardScoresPage> GetScore(GetScoresOptions options)
        {
            if (AuthenticationService.Instance.AccessToken == null) return null;
            try
            {
                var response = await LeaderboardsService.Instance.GetScoresAsync(LEADERBOARD_ID, options);
                return response;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
