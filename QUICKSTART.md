## Reveal data via VGS Forward Proxy (quick start)

1) upload the YAML `outbound-reveal-cvc-card.yaml` this will setup an echo server to test with `https://echo.apps.verygood.systems/post` and reveal `On response`.

2) `git clone git@github.com:verygoodsecurity/vgs-foward-proxy-example.git`

3) From within the controller file:
```
vgs-foward-proxy-example > vgs-card-example-asp-net > Controllers > PostController.cs
```
Please set the proxy by updating your contoller with your `username`, `password`, and `tenant id` from within your VGS Dashboard.
```
NetworkCredential credentials = new NetworkCredential("<username>", "<password>");
WebProxy proxy = new WebProxy("http://<tenant id>.sandbox.verygoodproxy.com:8080", false);
```

4) Note that you'll need to update the Upstream Host you'll want to actually go to for your server, but for now we'll leave our echo server in place.
```
HttpResponseMessage response = await httpClient.PostAsync("https://echo.apps.verygood.systems/post", content);
```

5) `docker build -t vgs-card-example-asp-net .`

6) `docker run -d -p 8080:80 --name myapp vgs-card-example-asp-net`

7) Go to `localhost:8080` and when done you can stop your Docker container with `docker stop myapp`.

NOTE 1: In order to properly have your project work with the VGS Forward Proxy, you will need to have the cert.pem file so remember to add this in your project.

NOTE 2: If you come across an error of Program does not contain a static 'Main' method suitable for an entry point while containerizing your project, please move your `Dockerfile` up a level from the folder with your `.csproj` file.
