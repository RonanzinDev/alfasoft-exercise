# Alfa Soft Test

#### This repository was made to solve the tests proposed by Alfa Soft.

<br>

#### The test is a console application, where we have to read a file with users, then consume an api (provided in the test) with these users, and save the answer in a log file.

<br>

### First step

You have to download the project and open the repositorie folder:

```shell
git clone https://github.com/RonanzinDev/alfasoft-exercise && cd alfasoft-exercise
```

After, you have to restore the project:

```dotnet
dotnet restore
```

## Using the Application

With the command `dotnet run`, the console will print a message like this "Please enter the file name:". Then we have to pass the path where the file that contains the names of the users is. Second the console will print the output with the response from the api and after the file 'log.log' will be create with the log messages from de response.
