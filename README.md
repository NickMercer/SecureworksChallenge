# Secureworks Coding Challenge

## **Challenge Instructions:**

1. Read input from a file of words;
2. Find the largest word in the file
3. Transpose the letters in the largest word
4. Show the largest word and the largest word transposed 
5.  Please use automated test cases using a test framework
6. Demonstrate positive and negative test cases
7. Your challenge should allow the user to pass in a file
8. Ensure you document code and instructions for building and running based on the response best practices above

 
### _Example File:_
a<br/>
ab<br/>
abc<br/>
abcd<br/>
abcde<br/>
 
### _Example Output:_
abcde<br/>
edcba<br/>

## **Assumptions:**

I made the following assumptions when writing the program and test suite:

1. All input files or files in input directories would be .txt files.

2. Any individual line in the file would not be larger than the available memory on the machine. (As in, it is safe to read an entire line from the input file into memory at once, though it may not be safe to read the entire file in at once.)

3. The file path is accessible to the program (e.g. Not blocked by access permissions, not locked, not accessed by any other means than a local file system path.) If the user does not have access to a file, the program will act as if the file doesn’t exist.

4. Words are denoted by space, tab, or newline characters.

5. Words only contain letters or apostrophes. Words containing other characters will not be eligible to be the longest word. Words with apostrophes will be counted, but the apostrophe does not factor into the word length.

6. Ties for longest word go to the first word found. Subsequent words need to be longer in order to be considered the longest word.

## **Platforms:**
This solution was developed and tested on:<br/>
	Windows 11 Home Edition<br/>

And Tested on: <br/>
	Rasbian Buster 10 (Raspberry Pi 3)<br/>
	macOS Monterey 12.2.1 (Apple M1)<br/>

## **Solution Description:**

Solution is a Console program that takes in a .txt file or directory path and returns the longest word in the file(s) and the longest word transposed to the console. The control flow for the program can be found in Program.cs in the TransposeLongestWord method. The method validates first that the argument given is a valid file or directory path, and throws Argument or FileNotFound exceptions if this is not the case. It then goes through the file(s) line by line, only reading one line into memory at a time, and gets the longest word in the text. Words with numbers, special characters, or punctuation other than apostrophes are thrown out. Words with apostrophes are counted, but the apostrophe does not count towards the total letters in the word. Ties between words are decided by preserving the existing longest word. Subsequent words need to be longer to be chosen instead of the current word. Finally, the program turns the longest word into a char array, reverses it, and prints both the longest word, and the transposed word to the console before exiting.

Future improvements could be made to the program to read lines via a stream, to avoid an out of memory exception if a line in a file was larger than the available memory. The program could also benefit from looking at the file types being input and filter out files other than .txt files. Also, replacing the exception messages with user friendly error messages would be an improvement. The test suite could be improved by including tests for large files/lines in files.


## Tests Description

The WordTransposerTests directory contains a suite of unit and integration tests, as well as test files/directories used by the integration tests. The Unit tests are separated into separate files for each module in the program. GuardTests houses unit tests for various guard clauses that throw exceptions on invalid input. FileServiceTests house unit tests for reading file system input, using a mock file system. The LongestWordServiceTests are unit tests for selecting the longest word out of a selection of lines of text. Finally, TranspositionServiceTests unit test the method of transposing words. In addition to the unit tests, the IntegrationTests class run the whole program under various input circumstances, valid and invalid, to test how the program responds to actual file input and verify correct output to the console. Since running the tests requires the dotnet sdk to be installed, a docker container has been hosted on docker hub that will spin up a version of the .net 6 sdk, build the project, and run the test suite.

## **Pre-Requisites:**

### To Run the Program:
Some way to extract .zip files.
Possibly administrator privileges on your machine.

### To Run the Tests:
Docker (Or a web browser)

## **Instructions:**

### To Run the Program:

1. Download the correct v1.0.0 zip for your operating system from the [releases page](https://github.com/NickMercer/SecureworksChallenge/releases/tag/v1.0.0). <br/>
(Linux and OSX releases are self-contained, but the Windows release will require having the .net runtime installed on your windows pc.)

2. Unzip the folder and navigate to the location in a terminal.

	Note: 
	On OSX/Linux you may need to give permissions for the executable
	
	OSX:
	```
	chmod +x ./WordTransposer
	```
	
	*If on OSX, you may also have a security issue with the dylibs and dlls. A work around for this is to go to the "Security & Privacy" panel of the macos system prefs, and allow each of the dylibs and dlls.*
	
	*Alternatively, after Gatekeeper blocks the app, you can go into Security & Privacy -> General and hit the 'Open Anyway' button that should appear next to the notification that the program was blocked.*
	
	*Finally, an insecure option is to temporarily disable gatekeeper to run the app. Unfortunately, I don't have a way to sign the program with Apple, so Gatekeeper is going to try to block it.* 
	
	
	Linux:
	```
	chmod 777 ./WordTransposer
	```

3. Then you can run the program. Each distribution has a test file included in the folder so you can quickly test the program with these commands:

	Windows:
	```
	.\WordTransposer "TestFile.txt"

	.\WordTransposer "TestFile.txt"
	```

	Mac/Linux:
	```
	./WordTransposer "TestFile.txt"

	./WordTransposer "TestFile.txt"
	```



### To Run the Tests


1. With Docker installed on your machine, or on [Play with Docker](https://labs.play-with-docker.com); run the image

	```
	docker run natick/word-transposer-tests
	```
	
	You should see the docker image build the source code/tests and run the unit/integration tests on the project. 

	Afterwards it is safe to remove the container:
	``` 
	docker rm {container-id}
	```
	
	and remove the image
	
	```
	docker rmi {image-id}
	```
	

