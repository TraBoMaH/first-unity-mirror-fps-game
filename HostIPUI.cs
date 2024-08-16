using Mirror;
using System.Net.Sockets;
using System.Net;
using TMPro;

public class HostIPUI : NetworkBehaviour
{
public TMP_Text playerIP;
 public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                playerIP.text = ip.ToString();
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
    private void Start() 
    {
        if(isServer)
        {
            GetLocalIPAddress();
        }
    }
}
