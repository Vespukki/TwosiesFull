using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DebugMeter
{
    public class SpeedMeter : MonoBehaviour
    {
        [SerializeField] RectTransform barHolder; //background
        [SerializeField] RectTransform bar; // one that moves
        [SerializeField] TextMeshProUGUI text;

        [SerializeField] Rigidbody2D body;
        [SerializeField] PlayerStats stats;

        private void Update()
        {
            bar.sizeDelta = new Vector2(Mathf.Abs(body.velocity.x) / stats.Speed * barHolder.sizeDelta.x, bar.sizeDelta.y);
            text.SetText(body.velocity.x.ToString("F4"));
        }
    }
}
