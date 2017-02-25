#!/bin/bash

# Make sure the kernel doesn't allow overcommit, so our process will really be
# killed when the system runs out of memory.
sudo sysctl vm.overcommit_memory=2

# Set up core files to be generated with the name 'core' in the current dir,
# and remove any limitation on their size.
sudo bash -c 'echo core > /proc/sys/kernel/core_pattern'
ulimit -c unlimited

# Restore, build, and run the web application.
dotnet restore
dotnet build
dotnet run

# Now, the list of bugs --
# 
# 1. To access a page that causes the server to crash in a  background thread
# after five seconds:
#	curl localhost:5000/Account/Login
#
# 2. To access a page that causes a 100MB memory leak, so that after a few
# repetitions the server will produce an OutOfMemoryException:
#	curl localhost:5000/Home/About
#
# 3. To access a page that causes a hang because of an internal deadlock in the
# server process, access the register page and click the Register button. It 
# doesn't matter what you put in the username and password fields.
