using Cysharp.Threading.Tasks;
using TMPro;
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
<<<<<<< HEAD
            //if (mini_list == null) throw new System.NullReferenceException("mini leaderboard");
            if (rank == null) throw new System.NullReferenceException("rank");
=======
            if (mini_list == null) throw new System.NullReferenceException("mini leaderboard");
            if (rank == null) throw new System.NullReferenceException("TMP_Text");
>>>>>>> c4c4627edfd338ac5470344ceaf5c97171dd17c5
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
<<<<<<< HEAD
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
            try
            {
                rank.SetText("");
                username.SetText("");
                score.SetText("");

                var scoreRespons = await leaderboard.GetScore(new GetScoresOptions { Offset = 0, Limit = showCount });
                if (scoreRespons == null) return;
                for (int i = 0; i < showCount && i < scoreRespons.Results.Count; i++)
                {
                    var scoredata = scoreRespons.Results[i];
=======
        {
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
            try
            {
                rank.SetText("");
                username.SetText("");
                score.SetText("");

                var scoreRespons = await leaderboard.GetScore(new GetScoresOptions { Offset = 0, Limit = showCount });
                if (scoreRespons == null) return;
                for (int i = 0; i < showCount && i < scoreRespons.Results.Count; i++)
                {
                    var scoredata = scoreRespons.Results[i];
>>>>>>> c4c4627edfd338ac5470344ceaf5c97171dd17c5
                    if (i != 0)
                    {
                        rank.text += "\n";
                        username.text += "\n";
                        score.text += "\n";
                    }
<<<<<<< HEAD
                    rank.text += scoredata.Rank + 1 + "st.";
=======
                    rank.text += scoredata.Rank + "st.";
>>>>>>> c4c4627edfd338ac5470344ceaf5c97171dd17c5
                    username.text += scoredata.PlayerName;
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
