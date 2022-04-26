# allegro-summer-experience-2022
My email in recruitment process: mateusz0990@gmail.com
Stworzone przeze mnie API to serwer pośredniczący(proxy). Służy on do pobierania danych z GitHuba poprzez zapytania do odpowiednich REST Endpoints udostępnianych przez Api GitHuba ( https://docs.github.com/en/rest/overview/endpoints-available-for-github-apps ). W swoim projekcie wykorzystuję bibliotekę Swagger która umożliwia dokumentowanie oraz korzystanie z dostępnego w aplikacji API.

# Controllers

## ControllerRepositories   
Odpowiada on za zwrócenie listy repozytoriów (nazw) wraz z informacjami o użytych w nich językach programowania.

## ControllerUSer  
Odpowiada on za zwrócenie danych użytkownika wraz z zagregowaną informacją o językach programowania użytych w jego repozytoriach.

# Instrukcja uruchomienia
Należy pobrać pilk WebAPI.zip, a następnie go rozpakować i uruchumoić za pomocą narzędzia Visual Studio. Uruchomienie projektu spowoduje uruchomienie się okna z 
dokumentacją metod udostępnianych przez API wykonaną przez swaggera, przy pomocy którego można wywołać i przetestować każdą z metod.
