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
	Rasbian Buster 10<br/>
	Mac OSX<br/>

## **Solution Description:**

(In Progress)

## **Pre-Requisites:**

### To Run the Program:
Some way to extract .zip files.

### To Run the Tests:
Docker (Or a web browser)

## **Instructions:**

### To Run the Program:

1. Download the correct v1.0.0 zip for your operating system from the [releases page](https://github.com/NickMercer/SecureworksChallenge/releases/tag/v1.0.0). <br/>
(Linux and OSX releases are self-contained, but the Windows release will require having the .net runtime installed on your windows pc.)

2. Unzip the folder and navigate to the location in a terminal.

	Note: 
	On Linux you may need to give permissions for the executable
	```
	chmod 777 ./WordTransposer
	```

3. Then you can run the program. Each distribution has a test file included in the folder so you can quickly test the program with these commands:

	Windows:
	```
	.\WordTransposer "TestFile.txt"

	.\WordTransposer "TestFile.txt"
	```

	Linux:
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


