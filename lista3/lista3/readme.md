Używamy metody asynschronicznej aby później przy odpytywaniu API skorzytać z słowa kluczowego await które 
powodouje, że proces zaczeka do końca zapytania przed uruchomieniem kolejnego.


Główną częścią skryptu jest metoda "FetchAndParseApiData" która w pierwszej kolejności tworzy obiekt klienta HTTP i używając go(słowo klucz "using" które zapewnia poprawne zamknięcie procesu po skończeniu odpytania) wykonuje zapytanie do API. 

Następnie korzystając z wbudowanej biblioteki HTTP sprawdzamy czy zapytanie zakończyło się sukcesem i sprawdzamy kod odpowiedzi zwrócony przez serwer.

Po otrzymaniu zwrotki z API korzystamy z biblioteki JObject która parse'uje otrzymaną odpowiedź
