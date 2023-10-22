﻿using AudioPlayer.Other;
using System.Collections.Generic;
using UnityEngine;
using VoiceChat;
using static AudioPlayer.Plugin;
using Extensions = AudioPlayer.Other.Extensions;

namespace AudioPlayer.API;
public static class AudioController
{
    public static void PlayAudioFromFile(string path, bool loop = false, float volume = 100, VoiceChatChannel channel = VoiceChatChannel.Intercom, bool shuffle = false, bool logdebug = false, bool Continue = true, int id = 99)
    {
        if (Extensions.TryGetAudioBot(id, out FakeConnectionList hub))
        {
            var audioPlayer = hub.audioplayer;
            audioPlayer.BroadcastChannel = channel;
            audioPlayer.Volume = volume;
            audioPlayer.Loop = loop;
            audioPlayer.Shuffle = shuffle;
            audioPlayer.Continue = Continue;
            audioPlayer.LogDebug = logdebug; //Welcome to Error spam ZONE!
            audioPlayer.Enqueue(path, -1);
            audioPlayer.Play(0);
        }
    }
    public static void SpawnDummy(int id, bool showplayer = false, string badgetext = "AudioPlayer BOT", string bagdecolor = "orange", string name = "Dedicated Server")
        => Extensions.SpawnDummy(name, showplayer, badgetext, bagdecolor, id);
    public static void StopPlayerFromPlaying(List<int> players, int id = 99)
    {
        if (Extensions.TryGetAudioBot(id, out FakeConnectionList hub))
            hub.audioplayer.BroadcastTo.RemoveAll(players.Contains);
    }
    public static void PlayFromFilePlayer(List<int> players, string path, bool loop = false, float volume = 100, VoiceChatChannel channel = VoiceChatChannel.Intercom, bool shuffle = false, bool logdebug = false, bool Continue = true, int id = 99)
    {
        if (Extensions.TryGetAudioBot(id, out FakeConnectionList hub))
        {
            var audioPlayer = hub.audioplayer;
            audioPlayer.BroadcastTo = players;
            audioPlayer.BroadcastChannel = channel;
            audioPlayer.Volume = volume;
            audioPlayer.Loop = loop;
            audioPlayer.Shuffle = shuffle;
            audioPlayer.Continue = Continue;
            audioPlayer.LogDebug = logdebug;
            audioPlayer.Enqueue(path, -1);
            audioPlayer.Play(0);
        }
    }
    public static void AddAudioEnqueue(string audio, int pos, int id = 99)
    {
        if (Extensions.TryGetAudioBot(id, out FakeConnectionList hub))
            hub.audioplayer.Enqueue(audio, pos);
    }
    public static void LogDebugAudio(bool logdebug = true, int id = 99)
    {
        if (Extensions.TryGetAudioBot(id, out FakeConnectionList hub))
            hub.audioplayer.LogDebug = logdebug;
    }
    public static void ContinueAudio(bool Continue = true, int id = 99)
    {
        if (Extensions.TryGetAudioBot(id, out FakeConnectionList hub))
            hub.audioplayer.Continue = Continue;
    }
    public static void ShuffleAudio(bool shuffle = false, int id = 99)
    {
        if (Extensions.TryGetAudioBot(id, out FakeConnectionList hub))
            hub.audioplayer.Shuffle = shuffle;
    }
    public static void StopAudio(int id = 99, bool clearAudioList = true)
    {
        if (Extensions.TryGetAudioBot(id, out FakeConnectionList hub))
        {
            hub.audioplayer.Stoptrack(clearAudioList);
            if (plugin.LobbySong != null)
                plugin.LobbySong = null;
        }
    }
    public static void LoopAudio(bool loop, int id = 99)
    {
        if (Extensions.TryGetAudioBot(id, out FakeConnectionList hub))
        {
            hub.audioplayer.Loop = loop;
        }
    }
    public static void VolumeAudio(float volume, int id = 99)
    {
        if (Extensions.TryGetAudioBot(id, out FakeConnectionList hub))
            hub.audioplayer.Volume = volume;
    }
    public static void DisconnectDummy(int id)
    {
        if (Extensions.TryGetAudioBot(id, out FakeConnectionList hub))
        {
            Object.Destroy(hub.hubPlayer.gameObject);

            try
            {
                hub.hubPlayer.OnDestroy();
            } catch { }

            FakeConnectionsIds.Remove(id);
        }
    }
}
