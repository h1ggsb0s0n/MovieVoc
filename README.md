# Wilkommen zu Movie Voc

Die folgenden Aktionen können derzeit durchgeführt werden:
Movie Voc ist eine Applikation welche es ermöglicht  Vokabular von Filmen zu lernen. <br />
Die folgenden Aktionen können derzeit durchgeführt werden:<br />

##User Interaktionen:<br />
* Benutzer kann einen Film suchen
* Benutzer kann einen Film auswählen.
* Benutzer kann das Vokabular eines Filmes anzeigen.
* Benutzer kann die Übersetzung eines Wortes anzeigen.
* Wörter als bekannt markieren.
* Wörter als unbekannt markieren.


##LoginSysteme:

* Registrierung
* Claims (Admin und Member)



##Verwendete Technologien:
* C#
* Blazor (Webassembly)
* BlazorMaterials
* BUnit
* MSSQL
* EntityFramework
* IdentityServer4


# Getting Started:

1. Download Visual Studio 2019 (Nicht Visual Studio Code)
2. Erstellen der Datenbank
   * Tools->NugetPackageManager
   * Command: Update-Database
3. Wenn nötig fehlende NugetPackages installieren.
4. Applikation Starten mit ILS Express
5. In der Applikation einen User registrieren.
6.  Das Login ist noch nicht komplett fertiggestellt daher müssen die Rollen der User von Hand definiert werden. </br>
    Dafür wird die dbo.ASpNetUserClaims Tabelle folgendermassen angepasst:
    * Userid von dem von **Ihnen** erstellten user kopieren. (In der dbo.AspNetUsers Tabelle)
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
10. Mit dem Lernen beginnen. (Movie Suchen -> Schwierigkeitsgrad wählen -> Lernen)






