using Mirror;
using System.Net.Sockets;
using System.Net;
using TMPro;

public class Connect : NetworkBehaviour
{
public NetworkManager manager;
public TMP_Text playerIP;
public TMP_InputField input;
private string ip;
private void Start() 
{
    ip = GetLocalIPAddress();
    playerIP.text = ip;
} 
public void Host()
{
    NetworkManager.singleton.networkAddress = ip;
    manager.StartHost();
}
 public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                //hintText.text = ip.ToString();
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
    public void Join()
    {
        NetworkManager.singleton.networkAddress = input.text;
        manager.StartClient();
    }
}
