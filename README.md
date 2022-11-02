# wyBinUpdater
Compiler and updater for various applications

## Instructions
- Create a ssh key (use `ssh-keygen` in a console) on the host computer
  - You can skip this step if you already have a generated ssh key
- Copy the contents of `id_rsa.pub`, this is located in `C:\Users\USER\.ssh`
- Connect to the wyBin server and go to `~/.ssh`
- Check if the file `authorized_keys` exists, if it doesn't create it (`touch authorized_keys`)
- Add the contents of the `id_rsa.pub` you copied earlier to the `authorized_keys` file
  - Make sure that ssh keys are all on separate lines

You should now be able to connect to the wyBin server and transfer files to it.

## wyBin front-end
Compiles the wyBin front-end and uploads it to the wyBin server.

### Requirements
- npm
- Angular cli

## wyBin back-end
Compiles the wyBin back-end and uploads it to the wyBin server.

### Production properties
Make sure to have the production properties file setup. The file should be located here: `api\src\main\resources\application-prod.properties`

### Requirements
- Java

## DirkBot
Compiles DirkBot and uploads it to the wyBin server.

### Production properties
Make sure to have the production properties file setup. The file should be located here: `src\main\resources\application-prod.properties`

### Requirements
- Java

## wyBinBot
Compiles wyBinBot and uploads it to the wyBin server.

### Production properties
Make sure to have the production properties file setup. The file should be located here: `src\main\resources\application-prod.properties`

### Requirements
- Java
