Imports System.Net.Http
Imports Newtonsoft.Json.Linq

Public Class Form1
    Private Async Sub GetWeatherButton_Click(sender As Object, e As EventArgs) Handles GetWeatherButton.Click
        Dim city As String = CityTextBox.Text.Trim()
        Dim apiKey As String = "YOUR_API_KEY" ' Replace with your OpenWeatherMap API key

        If Not String.IsNullOrWhiteSpace(city) Then
            Try
                Dim httpClient As New HttpClient()
                Dim response As HttpResponseMessage = Await httpClient.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric")
                Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()
                Dim data As JObject = JObject.Parse(jsonResponse)

                If response.IsSuccessStatusCode AndAlso data("cod").ToString() = "200" Then
                    Dim weatherDesc As String = data("weather")(0)("description").ToString()
                    Dim temperature As Double = data("main")("temp").ToObject(Of Double)()
                    WeatherLabel.Text = $"Weather: {weatherDesc}{Environment.NewLine}Temperature: {temperature}°C"
                Else
                    WeatherLabel.Text = "City not found."
                End If
            Catch ex As Exception
                WeatherLabel.Text = "An error occurred."
            End Try
        Else
            WeatherLabel.Text = "Please enter a city."
        End If
    End Sub
End Class