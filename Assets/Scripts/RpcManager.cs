using UnityEngine;
using UnityEngine.SceneManagement;

public class RpcManager : MonoBehaviour
{
    private DiscordRpc.EventHandlers handlers;
    DiscordRpc.RichPresence presence;

    private void Start()
    {
        handlers = new DiscordRpc.EventHandlers();
        DiscordRpc.Initialize("638998470182830085", ref handlers, true, "");
        presence = new DiscordRpc.RichPresence
        {
            details = "Idling..."
        };
        DiscordRpc.UpdatePresence(presence);
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame(int partySize)
    {
        presence.partySize = partySize;
        presence.partyMax = 4;
        presence.details = "Quick Play";
        presence.state = "In a party";
        DiscordRpc.UpdatePresence(presence);
    }

    private void OnDisable()
    {
        DiscordRpc.Shutdown();
    }
}
