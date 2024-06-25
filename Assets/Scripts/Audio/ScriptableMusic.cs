using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableMusicGroup", menuName = "MusicGroup")]
public class ScriptableMusic : ScriptableObject
{
    [SerializeField] private List<AudioClip> s_groupTracks = new List<AudioClip>();
    public List<AudioClip> S_groupTracks { get { return s_groupTracks; } }
}
