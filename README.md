# ManageRewards

Implemented the application using .NET 5.0. Published the required files (as framework dependent, this will not require any installation of runtime beforehand) into publish folder, which also includes required runtime for Windows/MacOS/Linux. Used SQLite as database for this application and it has a fixed connection string for the application, which cannot be the same in MacOS and Linux in this case, so make sure to run this application on a windows machine. Below the are the steps required to run this application.
1.	Install .NET 5.0 Runtime for Windows using the below link. Get the Hosting Bundle
https://dotnet.microsoft.com/en-us/download/dotnet/5.0
 
 ![image](https://user-images.githubusercontent.com/47949198/167664280-00247560-2ef8-4145-9f6a-17995f23091e.png)

2.	Get the git repository ManageRewards and save it on local disk

![image](https://user-images.githubusercontent.com/47949198/167664315-3d5740d2-00cd-407b-be44-24626f9c652d.png)

3.	Before proceeding to run the application, SQLite must be installed. Use the below link for downloading it https://www.sqlite.org/download.html
    Get sqlite-tools-win32-x86-3380500 zip file for Windows 
    Also get the SQLite Studio using the below link https://sqlitestudio.pl/#:~:text=Cross%2Dplatform,Windows%2C%20Linux%20and%20MacOS%20X.
    Make sure to create a folder SQLite within C drive(C:\SQLite)(Mentioned a specific path because, using this as part of a connection string for the database, this is not possible with MacOS and Linux). Copy the sqlite tools and studio files to this folder and unzip them.
    • Launch SQLite Studio
    
    • On the top menu, Select Database and click on Add a Database
    
    • Within the dialog opened, click on the plus sign to create a new database file

    ![image](https://user-images.githubusercontent.com/47949198/167664381-37199c25-9168-4445-8b06-347dec808e9c.png)

    Create a database with name RewardsDatabase

    ![image](https://user-images.githubusercontent.com/47949198/167664415-2ea29957-f78f-48c7-915e-bccc952dbd1d.png)

    Click on Ok once done.
    
    • Within SQLite Studio, click on the RewardsDatabase and right-click on it to see the menu. Click on Connect to the database.
    
    • Select the RewardsDatabase on the left menu, right-click on it to see the menu. Click on Execute SQL from File
    
    • Select TableCreation.sql from the repository, this will execute the query and create the table in RewardsDatabase
    
    At this point, the database is ready.
    
4.	To run the application in any OS, get the publish folder from the GitHub repository and copy it on to your local machine.

5.	Run command prompt, navigate to the publish folder on your disk. Run the below command. dotnet ManageRewards.dll
    This should show you information about the URL to launch the application.

6.	Launch Firefox browser for Windows, run http://localhost:5000

7.	If the app does not load(if the browser is blank), make sure Http/2 is disabled.

8.	These are the created routes

    ![image](https://user-images.githubusercontent.com/47949198/167664614-4c9ebfd4-77ae-4128-8554-584ed562ca90.png)

 
    • POST /Points for adding a transaction
    
    ![image](https://user-images.githubusercontent.com/47949198/167664693-0765f2eb-d311-48d1-8dbb-6e93c72ed154.png)

    • GET /Points to retrieve the points information
    
    ![image](https://user-images.githubusercontent.com/47949198/167664727-efc1b2aa-d372-4966-9223-fa562f8d21c6.png)

    • GET /Points/Spend/{points} to spend the available points 
    
    ![image](https://user-images.githubusercontent.com/47949198/167664760-48c7abd0-f826-4e1e-a3ab-953b51289c06.png)

