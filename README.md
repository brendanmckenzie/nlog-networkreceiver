nlog-networkreceiver
====================

A simple receiver for applications using NLog and the Network target

The inspiration for this was a project I am working on that will be a website running across a number of webservers and I want errors occuring on each webserver to be logged to a central location.  NLog provides the functionality to log to a Network device however I couldn't find an existing implementation.  Probably because it's fairly simple as you can see in the code.