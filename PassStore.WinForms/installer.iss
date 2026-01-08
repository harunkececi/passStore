; Inno Setup Kurulum Scripti
; PassStore Uygulaması için kurulum dosyası oluşturur
; Inno Setup Compiler ile derlenir: https://jrsoftware.org/isinfo.php

#define MyAppName "PassStore"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Harun KEÇECİ"
#define MyAppPublisherURL "mailto:kececi.harun@gmail.com"
#define MyAppExeName "PassStore.WinForms.exe"
#define MyAppId "{{A1B2C3D4-E5F6-7890-ABCD-EF1234567890}"

[Setup]
; Uygulama bilgileri
AppId={#MyAppId}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppPublisherURL}
AppSupportURL={#MyAppPublisherURL}
AppUpdatesURL={#MyAppPublisherURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=
InfoBeforeFile=
InfoAfterFile=
OutputDir=..\Installer
OutputBaseFilename=PassStore_Setup_{#MyAppVersion}
SetupIconFile=app.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
ArchitecturesInstallIn64BitMode=x64

[Languages]
Name: "turkish"; MessagesFile: "compiler:Languages\Turkish.isl"
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 6.1; Check: not IsAdminInstallMode

[Files]
; Ana uygulama dosyası ve tüm bağımlılıklar
Source: "bin\Release\net8.0-windows\PassStore.WinForms.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\net8.0-windows\*.dll"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "bin\Release\net8.0-windows\*.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\net8.0-windows\app.ico"; DestDir: "{app}"; Flags: ignoreversion
; Veritabanı şablonu (opsiyonel - ilk kurulumda oluşturulacak)
; Source: "bin\Release\net8.0-windows\passstore.db"; DestDir: "{app}"; Flags: ignoreversion onlyifdoesntexist

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\app.ico"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon; IconFilename: "{app}\app.ico"
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon; IconFilename: "{app}\app.ico"

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
// Forward declaration
function IsDotNetInstalled(version: String; var installedVersion: String): Boolean;
forward;

function IsDotNetInstalled(version: String; var installedVersion: String): Boolean;
var
  key: String;
  release: Cardinal;
  success: Boolean;
begin
  Result := False;
  installedVersion := '';
  
  // .NET Core/5+/6+/7+/8+ için kontrol
  // HKEY_LOCAL_MACHINE\SOFTWARE\dotnet\Setup\InstalledVersions\x64\sharedhost
  key := 'SOFTWARE\dotnet\Setup\InstalledVersions\x64\sharedhost';
  if RegKeyExists(HKLM, key) then
  begin
    // .NET 8.0 için spesifik kontrol
    if version = '8.0' then
    begin
      // .NET 8.0 için release numarası kontrolü
      // .NET 8.0 release numarası: 533325 veya üzeri
      key := 'SOFTWARE\WOW6432Node\dotnet\Setup\InstalledVersions\x64\sharedhost';
      if RegQueryDWordValue(HKLM, key, 'Version', release) then
      begin
        // Release numarası 533325 veya üzeri ise .NET 8.0 yüklü
        if release >= 533325 then
        begin
          Result := True;
          installedVersion := '8.0';
        end;
      end;
      
      // Alternatif kontrol: HKEY_LOCAL_MACHINE\SOFTWARE\dotnet\Setup\InstalledVersions\x64\sharedfx\Microsoft.NETCore.App
      if not Result then
      begin
        key := 'SOFTWARE\dotnet\Setup\InstalledVersions\x64\sharedfx\Microsoft.NETCore.App';
        if RegKeyExists(HKLM, key) then
        begin
          // 8.0.x versiyonlarını kontrol et
          if RegQueryStringValue(HKLM, key, '8.0', installedVersion) then
            Result := True;
        end;
      end;
    end;
  end;
  
  // Eğer hala bulunamadıysa, basit bir kontrol yap
  if not Result then
  begin
    key := 'SOFTWARE\dotnet\Setup\InstalledVersions\x64\sharedhost';
    Result := RegKeyExists(HKLM, key);
    if Result then
      installedVersion := 'Installed (version unknown)';
  end;
end;

function InitializeSetup(): Boolean;
var
  dotnetVersion: String;
begin
  // .NET 8.0 Runtime kontrolü
  if not IsDotNetInstalled('8.0', dotnetVersion) then
  begin
    MsgBox('PassStore uygulaması .NET 8.0 Runtime gerektirir.' + #13#10 + #13#10 +
           'Lütfen önce .NET 8.0 Runtime''ı yükleyin:' + #13#10 +
           'https://dotnet.microsoft.com/download/dotnet/8.0' + #13#10 + #13#10 +
           'Alternatif olarak Self-Contained kurulum dosyasını kullanabilirsiniz ' +
           '(PassStore_Setup_1.0.0_SelfContained.exe)', mbCriticalError, MB_OK);
    Result := False;
  end
  else
  begin
    Result := True;
  end;
end;
