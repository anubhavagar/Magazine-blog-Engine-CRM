<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="AapkiMagazine.Azure" generation="1" functional="0" release="0" Id="432dde54-ef2e-4149-a470-75404a890e9a" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AapkiMagazine.AzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="AapkiMagazine:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/LB:AapkiMagazine:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/LB:AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="AapkiMagazine:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/MapAapkiMagazine:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/MapAapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/MapAapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/MapAapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/MapAapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/MapAapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </maps>
        </aCS>
        <aCS name="AapkiMagazineInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/MapAapkiMagazineInstances" />
          </maps>
        </aCS>
        <aCS name="Certificate|AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/MapCertificate|AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:AapkiMagazine:Endpoint1">
          <toPorts>
            <inPortMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazine/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput">
          <toPorts>
            <inPortMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazine/Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazine/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapAapkiMagazine:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazine/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapAapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazine/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapAapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazine/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapAapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazine/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapAapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazine/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapAapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazine/Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </setting>
        </map>
        <map name="MapAapkiMagazineInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazineInstances" />
          </setting>
        </map>
        <map name="MapCertificate|AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazine/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="AapkiMagazine" generation="1" functional="0" release="0" software="C:\Anubhav\projects\AapkiMagazine\AapkiMagazine\AapkiMagazine.Azure\csx\Release\roles\AapkiMagazine" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/SW:AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;AapkiMagazine&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AapkiMagazine&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazine/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazineInstances" />
            <sCSPolicyFaultDomainMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazineFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyFaultDomain name="AapkiMagazineFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="AapkiMagazineInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="72ed2bb7-7732-40c7-aacd-c909fe401c07" ref="Microsoft.RedDog.Contract\ServiceContract\AapkiMagazine.AzureContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="5fe646fe-576f-4f60-ad16-4db38e832d46" ref="Microsoft.RedDog.Contract\Interface\AapkiMagazine:Endpoint1@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazine:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="dab40d61-726d-42db-b64a-fe569ad17730" ref="Microsoft.RedDog.Contract\Interface\AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/AapkiMagazine.Azure/AapkiMagazine.AzureGroup/AapkiMagazine:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>