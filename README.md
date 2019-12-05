# XamlOpenUrl
A sample project to demo a simple markup extension for opening urls from Xamarin Forms xaml views. Less code, xaml hot reload goodness, all win!

```
<Button Text="Microsoft Learn" Command="{extensions:OpenUrl 'https://docs.microsoft.com/en-ca/learn/'}"/>
<!-- OR use a binding! -->
<Button Text="Microsoft Learn" Command="{extensions:OpenUrl}" CommandParameter="{Binding MicrosoftLearnUrl}"/>
```
