# Project 'Travel Agencies'

----

![Rider](https://img.shields.io/badge/Rider-000000.svg?style=for-the-badge&logo=Rider&logoColor=white&color=black&labelColor=crimson)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Xamarin](https://img.shields.io/badge/Xamarin-3199DC?style=for-the-badge&logo=xamarin&logoColor=white)

## Description

The application should help the employees of multiple Travel Agencies manage the offers for vacations.
Being used by multiple agencies, the application is using the Server - Client architecture. 
As a plus there is a Thrift module that can be used with a client built in another programming language.
The REST module is used just for testing, but can be extended to have a fully functional website. 

One offer has:
- a landmark to visit
- a transport company's name
- a departure time
- a price
- a number of slots for the trip

A user can:
- Log in
- Log out
- Filter the offers based on:
    - destination and departure hour interval
- Make reservations on an offer

Every change from a user is updated live to all the connected users. 
Data is stored in a database on the server.