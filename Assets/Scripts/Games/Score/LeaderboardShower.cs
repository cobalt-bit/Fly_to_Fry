using TMPro;
using Unity.Services.Leaderboards;
using UnityEngine;

namespace Score
{
    public class LeaderboardShower : MonoBehaviour
    {
        [SerializeField] LeaderboardManeger leaderboard;
        [SerializeField, Min(0)] int ShowCount;
        TMP_Text text;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (leaderboard == null) throw new System.NullReferenceException("LeaderboardManeger");
            text = GetComponent<TMP_Text>();
            if (text == null) throw new System.NullReferenceException("TMP_text");
        }

        public async void Show()
        {
            try
            {
                text.SetText("");
                var scoreRespons = await leaderboard.GetScore(new GetScoresOptions { Offset = 0, Limit = ShowCount });
                if (scoreRespons == null) return;
                for (int i = 0; i < ShowCount && i < scoreRespons.Results.Count; i++)
                {
                    var scoredata = scoreRespons.Results[i];
                    if (i != 0) text.text += "\n\n";
                    text.text += string.Format("{0:##}st. {1}\n{2:0.0}", i + 1, scoreRespons.Results[i].PlayerName, scoreRespons.Results[i].Score);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
