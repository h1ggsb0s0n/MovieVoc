# Wilkommen zu Movie Voc

Die folgenden Aktionen können derzeit durchgeführt werden:
Movie Voc ist eine Applikation welche es ermöglicht  Vokabular von Filmen zu lernen. <br />
Die folgenden Aktionen können derzeit durchgeführt werden:<br />

## User Interaktionen:<br />
* Benutzer kann einen Film suchen
* Benutzer kann einen Film auswählen.
* Benutzer kann das Vokabular eines Filmes anzeigen. (Momentan funktioniert die Funktion nur mit dem Default Value Schwierigkeit Alle)
* Benutzer kann die Übersetzung eines Wortes anzeigen.
* Wörter als bekannt markieren.
* Wörter als unbekannt markieren.
* Registrieren
* Anmelden
* Abmelden

Filme Suchen und Wörter Suchen funktionieren mit Typeahed. Nach 3 Buchstaben wird die Datenbank abfrage gestartet und Vorschläge werden gezeigt.

## LoginSysteme:

* Registrierung
* Claims (Admin und Member)



## Verwendete Technologien:
* C#
* Blazor (Webassembly)
* MatBlazor
* BUnit
* MSSQL
* EntityFramework
* IdentityServer4


# Getting Started:

1. Download Visual Studio 2019 (Nicht Visual Studio Code) -> Gt Repo clonen -> movievoc.sln mit Visual Studio öffnen
2. Erstellen der Datenbank
   * Tools->NugetPackageManager
   * Command: Update-Database
3. Wenn nötig fehlende NugetPackages installieren.
4. Applikation starten mit ILS Express (MovieVoc.Server -> ILS Express)
5. In der Applikation einen User registrieren.
6.  Das Login wurde noch nicht fertiggestellt. Daher müssen die Rolle Admin für den User von Hand definiert werden. </br>
    Dafür wird die dbo.ASpNetUserClaims Tabelle folgendermassen angepasst:
    * Userid von dem von **Ihnen** erstellten User kopieren. (In der dbo.AspNetUsers Tabelle)
    * ClaimType von der unteren Tabelle entnehmen
    * ClaimValue von der unteren Tabelle entnehmen.
<table>
  <thead>
    <tr>
      <th>UserId</th>
      <th>CLaimType</th>
      <th>ClaimValue</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>4ffbc526-7e5d-4aa5-b5e8-5711e866642f</td>
      <td>http://schemas.microsoft.com/ws/2008/06/identity/claims/role</td>
      <td>Admin</td>
    </tr>
  </tbody>
</table>

7. In der Applikation Movie erstellen (als Admin eingeloggt)
8. In der Applikation Wörter erstellen (als Admin eingeloggt)
9. In der Applikation Wörter einem Film zufügen. (als Admin eingeloggt)
10. Mit dem Lernen beginnen. (Movie Suchen -> Schwierigkeitsgrad Alle Wörter -> Lernen)


# Überlegungen zur Software

Die Idee zur Software stammt aus dem Modul SoftwareArchitektur. Die Software soll eine solide Basis zu einem ausbaubaren Projekt bieten. Als langfristige Ausbaustufe ist eine Mobilapplikation sowie die Implementierung einer Microservice Architektur geplant. Bei dieser Projektarbeit wollte ich mir hauptsächlich zusätzliches Wissen im Bereich IdentityServer, Entity Framework, Nunit testing mit Moq und Blazor aneignen.

# Bei mehr Zeit

Ursprünglich war mehr Testing geplant. Das Ziel war es auch E2E Testing mit Cypress zu integrieren. Leider hat der Grundaufbau zu lange gedauert. Auch im Bereich Datenbank wollte ich mehr machen. Beispielsweise fehlen wichtige Constraints im Backend. Auch die Rückgabetypen in den Controllern sind nicht immer vom gleichen Typ. Dies sollte vereinlicht werden. Es bestehen noch einige Buggs im Frontend. Schade war dass ich 2 zusätzliche Tutorials a je ca. 5h (UnitTesting und EntityFramework) durchgearbeitet habe. Ich konnte aufgrunde Zeitmangels nur wenig davon in die Applikation übertragen.

# Das würde ich anders machen

Beim Testing mit Moq ist teilweise eine andere Architektur nötig. Für den MovieController wurde eine weitere Abstraktionsebene (Datenbank) mit Interface ergänzt. Dadurch kann gemockt werden. Dies war mir vorher nicht bekannt. Der Umbau braucht einiges an Zeit. Dies würde ich beim nächsten Proejekt beachten. Am Anfang habe ich mit Normalen HTML Inputs gearbeitet. Ca. in der Mitte des Projektes habe ich auf MatBlazor umgestellt. Dies hat enorm viel Zeit ersparrt. Beim nächsten Projekt würde ich sofort mit MatBlazor starten.




