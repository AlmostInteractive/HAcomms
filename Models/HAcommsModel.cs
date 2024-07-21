using HAcomms.BrowserTools;
using HAcomms.Tools;
using Microsoft.Extensions.Configuration;
using NoeticTools.Net2HassMqtt;
using NoeticTools.Net2HassMqtt.Configuration;
using NoeticTools.Net2HassMqtt.Configuration.Building;

namespace HAcomms.Models;

using CommunityToolkit.Mvvm.ComponentModel;

// A Better way of doing the same thing as above ...
public partial class HAcommsModel : ObservableObject {
    [ObservableProperty] private bool _inGoogleMeets;
    
    private INet2HassMqttBridge? _bridge; 

    public INet2HassMqttBridge BuildBridge(IConfigurationRoot appConfig) {

        var device = new DeviceBuilder().WithFriendlyName("HAcomms")
            .WithId("hacomms")
            .WithFriendlyName("HAcomms");

        device.HasBinarySensor(config => config.OnModel(this)
            .WithStatusProperty(nameof(HAcommsModel.InGoogleMeets))
            .WithFriendlyName("In Google Meets")
            .WithNodeId("in_google_meets"));

        var mqttOptions = HassMqttClientFactory.CreateQuickStartOptions(Properties.Resources.MqttClientId, appConfig);
        return _bridge = new BridgeConfiguration()
            .WithMqttOptions(mqttOptions)
            .HasDevice(device)
            .Build();
    }

    public void UpdateStatus() {
        long start = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        var windows = WindowsTools.GetOpenWindows();
        
        var firefoxes = windows.Where(kvp => kvp.Value.Contains("Mozilla Firefox")).ToDictionary();
        var chromes = windows.Where(kvp => kvp.Value.Contains("Google Chrome")).ToDictionary();

        var chromeTabs = Chrome.GetAllTabTitles(chromes.Keys);
        this.InGoogleMeets = BrowserTabs.CheckTabsForMeetings(chromeTabs);
        Console.WriteLine(this.InGoogleMeets ? "In meeting" : "Not in meeting");
    }
}

