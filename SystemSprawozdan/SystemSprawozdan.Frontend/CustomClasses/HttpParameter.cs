namespace SystemSprawozdan.Frontend.CustomClasses;

public class HttpParameter
{
    public HttpParameter(string key, string? value)
    {
        Key = key;
        Value = value;
    }
    public HttpParameter(string key, int? value)
    {
        Key = key;
        Value = value?.ToString();
    }
    public HttpParameter(string key, bool? value)
    {
        Key = key;
        Value = value?.ToString();
    }
    public string Key { get; set; }
    public string Value { get; set; }

    public string AddToUrl(string url)
    {
        if (string.IsNullOrEmpty(Value)) 
            return url;
        
        if (!url.Contains("?"))
            return $"{url}?{Key}={Value}";
            
        if(url.Contains($"{Key}="))
        {
            var start = url.IndexOf($"{Key}=");
            var end = url.Substring(start).IndexOf("&");
            end = end == -1 ? url.Length - start : end;
                
            var replaceFragment = url.Substring(start, end);
            url = url.Replace(replaceFragment, $"{Key}={Value}");
            return url;
        }

        return $"{url}&{Key}={Value}";
    }
}