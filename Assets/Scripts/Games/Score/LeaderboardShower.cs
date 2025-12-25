using Cysharp.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using UnityEngine;

namespace Score
{
    public class LeaderboardShower : MonoBehaviour
    {
        [SerializeField] LeaderboardManeger leaderboard;
        [SerializeField, Min(0)] int miniShowCount;
        [SerializeField, Min(0)] int showCount;
        [SerializeField] TMP_Text mini_list;
        [SerializeField] TMP_Text rank;
        [SerializeField] TMP_Text username;
        [SerializeField] TMP_Text score;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (leaderboard == null) throw new System.NullReferenceException("LeaderboardManeger");
            if (rank == null) throw new System.NullReferenceException("TMP_Text");
            if (username == null) throw new System.NullReferenceException("username");
            if (score == null) throw new System.NullReferenceException("score");
        }

        public void CallShow() => Show().Forget();
        public async UniTask Show()
        {
            await SetMiniList();
            await SetRanking();
        }

        async UniTask SetMiniList()
        {
            if (mini_list == null) return;
            try
            {
                mini_list.SetText("");
                var scoreRespons = await leaderboard.GetScore(new GetScoresOptions { Offset = 0, Limit = miniShowCount });
                if (scoreRespons == null) return;
                for (int i = 0; i < miniShowCount && i < scoreRespons.Results.Count; i++)
                {
                    var scoredata = scoreRespons.Results[i];
                    if (i != 0) mini_list.text += "\n";
                    mini_list.text += string.Format("{0:##}st. {1:0.0}", i + 1, scoreRespons.Results[i].Score);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        async UniTask SetRanking()
        {
            rank.SetText("順位\n");
            username.SetText("名前\n");
            score.SetText("最高スコア\n");

            try
            {
                var scoreRespons = await leaderboard.GetScore(new GetScoresOptions { Offset = 0, Limit = showCount });
                if (scoreRespons == null) return;
                for (int i = 0; i < showCount && i < scoreRespons.Results.Count; i++)
                {
                    var scoredata = scoreRespons.Results[i];
                    if (i != 0)
                    {
                        rank.text += "\n";
                        username.text += "\n";
                        score.text += "\n";
                    }
                    rank.text += scoredata.Rank + 1 + "st.";
                    username.text += scoredata.PlayerName;
                    if (AuthenticationService.Instance.PlayerId == scoredata.PlayerId)
                        username.text += " (You)";
                    score.text += string.Format("{0:0.0}", scoredata.Score);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
