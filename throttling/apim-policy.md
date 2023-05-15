A sample policy to rate limit 10 calls for 20 seconds based on subscription key. The policy will set the remaining calls in a variable named `remainingCallsPerSubscription`.

```xml 
<rate-limit calls="10" renewal-period="20" remaining-calls-variable-name="remainingCallsPerSubscription" />
```

Sample APIM request with subscription key:

```http
GET https://apim-dev-dy-2023.azure-api.net/apim-demo-2023-05-15/SayHello HTTP/1.1
Host: apim-dev-dy-2023.azure-api.net
Ocp-Apim-Subscription-Key: {KEY}
```

```http
GET https://apim-dev-dy-2023.azure-api.net/apim-demo-2023-05-15/SayHello?subscription-key={KEY} HTTP/1.1
Host: apim-dev-dy-2023.azure-api.net
```

