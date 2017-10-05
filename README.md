# WebApplikasjoner
Semesteroppgave i faget WebApplikasjoner 5. semester på HiOA
Applikasjon for å kunne bestille flybilletter

Laget av: 
Andreas Strand s305036
Stian Grimsgaard s305537

Applikasjonen vår består av 4 forskjellige sider som brukeren navigerer seg gjennom for å bestillle en billett, samt 3 partial views hvor 2 av dem lastes inn dynamisk.

På Index-siden kan du velge ankomst og destinasjon (flyplass), antall passasjerer, enkel reise eller tur/retur og når man ønsker å reise. Feilmelding kommer opp om felt ikke er fylt inn riktig. Når man trykker på "Search flights" knappen blir et view lastet inn dynamisk som viser både direkte reiser og reiser med mellomlanding til brukeren. Dette viewet gjør forsiden mindre brukervennlig da brukeren må scrolle en del for å navigere den siden. Det skal da sies at dette er valgt å beholdes for å vise at vi kan utføre ajax-kall.

Etter valgt reise fyller man inn informasjon for passasjerene, med en reiseleder/betaler/kontaktperson, valgt reise vises øverst på siden.

Videre kommer man til en 'betalingsside' hvor kort-nummer, utløpsdate og CVC kreves. Denne informasjonen blir ikke lagret i systemet vårt da vi ikke ser nødvendigheten med dette, da denne 'funksjonaliteten' er for å gjøre det mer realistisk. Valgt reise vises her også øverst på siden.

Til slutt kommer brukeren til en bekreftelses-side, hvor hver billett vises som en rad i en tabell, samt et referansenummer for selve bestillingen.

Etter man har fått referansenummeret kan man søke opp bestillingen sin. Dette gjøres i header-menyen om man trykker på "search". Her kan man skrive inn referansenummer og informasjon om bestillingen kommer opp.


Løsningen er lagt til i Azure og finnes her: 

https://webapph17.azurewebsites.net/

Det kreves ingen log-in informasjon for å få tilgang til siden vår.

NB! Nederst på siden i footeren så ligger informasjon om flyvninger inne. Hovedsaklig så ligger alle flyvninger inne 12. og 13. september 2017. Søker man på Oslo - Paris 12. september med retur 13. september får man mulighet til å bestille to reiser med mellomlandinger.
