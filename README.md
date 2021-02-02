# SuperChat
This is a Realtime Web chat App that let multiple users to talk via conversation groups (Chat rooms) it also integrates with a stock Bot that get finantial data from 
[https://stooq.com/](https://stooq.com/).

The app was built using:
* ASP.NET Core 3.1
* ASP.NET Core Identity
* AutoMapper
* Microsoft SignalR
* RabbitMq
* MassTransit
* VueJs
* WIX Toolset

## Project Setup
1. Clone the repository and Open it with VS.
2. Unload Project **SuperChat.Bot.Installer**
  * This is because you need to install the [WIX Tool](https://wixtoolset.org/releases/) in your machine and [VS Extension](https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2019Extension) to enable build for this project.
3. Run your RabbitMq instance if you have one. If not run the following docker command to set up a container with RabbitMq
  ``` bash
  docker run -it --rm --name superchat-rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
  ``` 
4. Change Application Settings (For projects **SuperChat**, **SuperChat.API** and **SuperChat.Bot**)
  * **Database Setting**
    * The app is using LocalDb by default so you don't need to change this but if you wish you can change the ConnectionString "**DefaultConnection**" to match your SQL Server Instance.
  * **RabbitMq Setting**
    * If you ran the docker command above, you don't need to change this. but if you have your own RabbitMq instance this is the setting you need to change:
    * **Host** (your amqp connectionstring to RabbitMq, ex: '*amqp://guest:guest@localhost:5672*')
5. Set up **SuperChat** project as your start up project.
6. Open the Nuget Package Manager Console and Select **SuperChat.Datamodel** as the default project
7. Run update-database command
``` bash
  Update-Database
``` 
8. After Migration is Done, Right Click Solution and click "*Set Startup Projects*".
9. Click the "*Multiple startup projects*" radio button and select projects **SuperChat**, **SuperChat.API** and **SuperChat.Bot** to start. Hit the *Ok* Button.
10. Hit the Run button.

You must register to get an account and enter to the Chat room page.

If you want to create more Chat Rooms, use the API via Swagger.

### Installer
To test the installer just run the .MSI file and look for the program **SuperChat Bot** and executed. Then run the **SuperChat** Project from VS and chat with the bot.
