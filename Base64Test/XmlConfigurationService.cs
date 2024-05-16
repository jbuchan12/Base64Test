
using System.Xml.Linq;

namespace Base64Test;

internal class XmlConfigurationService
{
    private const string Xml = 
        """
        <?xml version="1.0" encoding="UTF-8"?>
            <ConfigData>
                <isRegistered>true</isRegistered>
                <useMail>false</useMail>
                <pop3_ssl>true</pop3_ssl>
                <pop3_starttls>true</pop3_starttls>
                <smtp_ssl>true</smtp_ssl>
                <smtp_starttls>true</smtp_starttls>
                <imap_ssl>true</imap_ssl>
                <imap_starttls>true</imap_starttls>
                <max_check_frequency>20</max_check_frequency>
                <blackbox>false</blackbox>
                <usbSerialNumber>13A4567972EE</usbSerialNumber>
                <dataBaseName>NAVTOR</dataBaseName>
                <navboxId>11376</navboxId>
                <NSfunctions>6</NSfunctions>
                <AISRepeaterPort>0</AISRepeaterPort>
                <GPSRepeaterPort>0</GPSRepeaterPort>
                <AISDownSampleThreshold>0</AISDownSampleThreshold>
                <GPSDownSampleThreshold>0</GPSDownSampleThreshold>
                <proxyPort>0</proxyPort>
                <routeSyncIntervalMinutes>1440</routeSyncIntervalMinutes>
                <ais_stream_quote>0</ais_stream_quote>
                <ecdis_route_export_enabled>1</ecdis_route_export_enabled>
                <ecdis_route_format>CirmRtz,Navtor</ecdis_route_format>
                <useFirewall>false</useFirewall>
                <aenp_sdk_enabled>false</aenp_sdk_enabled>
                <vessel_name>MS JONATHAN</vessel_name>
                <logAisClassB>true</logAisClassB>
                <is_460>false</is_460>
                <apiEnabled>false</apiEnabled>
                <rdp_enabled>false</rdp_enabled>
                <call_sign></call_sign>
                <imo_number>31152</imo_number>
                <navbox_api>
                    2135CA7DC0F8E704DB7D4F8CA2E8958C565E05576C3EEAD6E056D548A1B3768A,7DFE9A5C85DC047B6B97581C7BCD2AADB039C169F141EC3DB8F12AFA1FFB87FC,8B62A6358DF92E18C717D5B6D0189577F0BDE4E8E9BDD4D7E47FC7AFE7D12CA9,9D3527BC1F00011707DB4DE775B4C7C0EE08F50505F089C9E5C5BA99C5A00A87,A7592B4656AB7690CC6073253256143EDB4185F8A2D8EA6D6779F790B4252A61,ED6BF8ACEE65E9C5A90B432026CF720AC964149D95D7D7D97517F7A5D1C6F1C3
                </navbox_api>
                <com_auto_detect>0</com_auto_detect>
                <windows_update_running>0</windows_update_running>
                <antivirus_enabled>0</antivirus_enabled>
                <rest_api_enabled>1</rest_api_enabled>
                <soap_api_enabled>1</soap_api_enabled>
                <udp_poll_enabled>0</udp_poll_enabled>
                <webdav_enabled>0</webdav_enabled>
                <route_enc_share_formats></route_enc_share_formats>
                <log_vdr_data>0</log_vdr_data>
                <disable_account_lockout>0</disable_account_lockout>
                <large_message_package_size>204800</large_message_package_size>
                <ui_server_enabled>1</ui_server_enabled>
                <udp_poll_frequency>0</udp_poll_frequency>
                <use_message_encryption>0</use_message_encryption>
                <disable_http>0</disable_http>
                <reject_unencrypted_messages>0</reject_unencrypted_messages>
                <electronic_log_book>0</electronic_log_book>
                <autodiscover_enabled>1</autodiscover_enabled>
                <grpc_enabled>1</grpc_enabled>
                <fallback_addresses>navserver-sg.navtor.com,20.212.101.221</fallback_addresses>
                <automationDataReportIntervalMinutes>15</automationDataReportIntervalMinutes>
                <ais_streaming_address>navserver2.cloudapp.net</ais_streaming_address>
                <unencrypted_module_configurations>eyJDb25maWd1cmF0aW9ucyI6e319</unencrypted_module_configurations>
                <module_configurations>
                     bXNnZXZwWn+LVC+bMAGJxpbPOv9Ce+9eyeChZDCCw4rXf07T5+mCrPxG0WTd/arbv76RojkfLp9hSjNs+3aLxNWZdNTu6OqX1Gbf/uZ/dO401NmPealIdSpTXmksdNWlPzWVat+l3Zhv0eOkwHBgS/zyBM0pV0d0O2MTZC9Aj4vXtXQM4qh09R/lQwsO8+A/Eco+0DllSI7n44Wopbatl19XI6S4iPwmkLem6rxPwG4AFViAfCziWt8QUzRYo6Y1RoEO8Cz9sDGyB0pIOP+pzSRObZ6DSIuAbDyJFSVAvc+gyHE8ysejz54TGhJEwKtDXUl7PP9ekCGciJzkVmFnf7WBg5qRx2UjPGV/C8ASNRlBJF/8LqysyCJdGVq+1ikcd4iwKaUu4Xk+9qt1ReH+AOxjKsvsl0kyQyRzrTcH4mGC0M+Kp+AQVkloj2tYokHuI/W0xkVlN8jHGBpdMd8AKNcZHGRgx+BVjPHqRJjrM9ma0AEv2qjDPznHN32nnrjEsssjpYDzU7NBRVEzUdjz796/HpWj6Azqj6ypI2w6SEFgISsJ+SVUJGteofRkEJXJgeRLMImnNfIJliUrmRXG1jHrqrRlm8tYb8rKRR3HrkgiY+qCyi77oT5n+VhV1eM7Oqu4oHqrR5X+xCmq+gjHM8OPfqWA8vc2wI54hntKWYt0boRq6NOcvc0xfcd+TzjwLH95F1x+NpyP89r8baHeTDhNztsTwA==
                </module_configurations>
            </ConfigData>
        """;

    public static XDocument AddSubscriptionDataToXml()
    {
        XDocument document = XDocument.Parse(Xml);
        if(document.Root is null)
            return document;

        XElement configNode = document.Root;

        var subscriptionString = $"{Base64Integer.FromInteger(123).StringValue}|{DateTime.Now.Ticks}";
        
        configNode.Add(
            GetSubscriptionConfigXmlElement(
                new Subscription(subscriptionString).ToString()));

        return document;
    }

    private static XElement GetSubscriptionConfigXmlElement(string subscriptionData) 
        => new("SubscriptionData", subscriptionData);
}
