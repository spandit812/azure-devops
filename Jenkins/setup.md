1.  
```bash
sudo apt update && sudo apt upgrade -y  
sudo apt install openjdk-11-jdk -y  
java -version  

curl -fsSL https://pkg.jenkins.io/debian-stable/jenkins.io-2023.key | sudo tee /usr/share/keyrings/jenkins-keyring.asc > /dev/null  

echo "deb [signed-by=/usr/share/keyrings/jenkins-keyring.asc] https://pkg.jenkins.io/debian-stable binary/" | sudo tee /etc/apt/sources.list.d/jenkins.list > /dev/null  

sudo apt update
sudo apt install jenkins -y
sudo systemctl start jenkins
sudo systemctl enable Jenkins
sudo systemctl status Jenkins
sudo ufw allow 8080
sudo ufw reload
sudo -s
```

2.  
Add inbound rule to the VM, keeping port 8080.
3. 
Open in the browser: VM-PUBLIC-IP:8080
4. Go to the folder:
```bash
sudo -s
cat /var/lib/jenkins/secrets/initialAdminPassword
```
5. You need to install the required tools on the VM. For example, if you want to build angular, need angular and nodejs installed.  
If you need to run dotnet project, need to install dotnet sdk, and git to pull the project from the repo.
```bash
sudo apt install git
git --version
6. Give permission on the jenkins folder
sudo chmod -R a+rwx /var/lib/jenkins/
sudo chmod -R a+rwx /tmp/NuGetScratch/
```
7. For the linux distribution
* Go and create freestye job
* Give your username and password for the repository to access from the github.
* Build--> Execute Shel--> Write your command here
