```
 _____      _              _       _ _             _____ _ _            _       
/  ___|    | |            | |     | (_)           /  __ \ (_)          | |      
\ `--.  ___| |__   ___  __| |_   _| |_ _ __   __ _| /  \/ |_  ___ _ __ | |_ ___
 `--. \/ __| '_ \ / _ \/ _` | | | | | | '_ \ / _` | |   | | |/ _ \ '_ \| __/ __|
/\__/ / (__| | | |  __/ (_| | |_| | | | | | | (_| | \__/\ | |  __/ | | | |_\__ \
\____/ \___|_| |_|\___|\__,_|\__,_|_|_|_| |_|\__, |\____/_|_|\___|_| |_|\__|___/
                                              __/ |                             
                                             |___/                              
```

* https://github.com/GuidanceAutomation/SchedulingClients
* https://www.guidanceautomation.com/

# Release Notes

## v15.0.0 (11th Sep 20)

* Major refactor to ```IServiceResult``` and ```IServiceResult<T>``` implementation.
* Consistent suffix renaming of all data transfer objects to ```dto```.
* Uses ```GAAPICommon``` package for .NET Standard enumerator definitions.

## v14.0.1 (8th June 20)

* Strips out dead clients
    * Agent Attention
    * Agent Battery Status
    * Agent Statecast

## v14.0.0 (14th Oct 19)

* Support for GACore enumerator definitions
* MapClient controls

## v13.1.0 (16th Sep 19)

* Adds IJobStateClient control examples
* Adds ITaskStateClient control examples

## v13.0.0 (29th May 19)

* Supports occupying mandates.
* Replaces moving task with GoTo node.
* Supports atomic move lists and atomic moves.

## v12.1.2 (6th May 19)

* Built for AnyCPU.

## v12.1.1 (17th Apr 19)

* Supports moNitrav v5.8.x.x and later versions to support updated schema.

## v12.1.0 (8th Nov 18)

* Ability to set agent lifetime state via the agent client.

## v12.0.0 (1st Nov 18)

* Supports refactored enumerators and enumerated values for job and tasks statuses.

## v11.1.0 (17th Oct 18)

* Supports async behvaior of job and task abortion within moNitrav.

## v11.0.0 (26th Aug 18)

* Built for AnyCPU.

## v10.0.0 (31st Jul 18)

* Job progress updates.
* Task progress updates.
* Issuing directives via a parameter alias.
* Obtaining job summary including task summaries in a single call.

## v9.3.0 (20th Jun 18)

* Supports required base movement type argument.

## v9.2.0 (8th Jun 18)

* Rebuilt for x64 as issues currenty with AnyCPU.

## v9.1.0 (31st May 18)

* Suport for AnyCPU.

## v8.1.0 (24th Apr 18)

* Supports kingpin.xml download via fleet manager service.

## v8.0.0 (19th Apr 18)

* Supports tool chain for resolving tasks and jobs in a fault state.

## v7.2.0 (9th Apr 18)

* Adds time to stationary and last completed instruction id to kingpin state.

## v7.1.0 (4th Apr 18)

* Refactored fleet manager client which supports new fleet state callback and abililty to commit waypoints via comms.

## v7.0.0 (2nd Apr 18)

* Refactored to use generic clients.

## v6.0.0 (19th Mar 18)

* Update pipeline task creation with finalization arguments.

## v5.0.0 (15th Mar 18)

* Adds support to enable in progress jobs to be edited.

## v4.1.1 (30th Jan 18)

* UDP budfix for incorrect serialization of headings.

## v4.1.0 (24th Jan 18)

* Updated task support for
** Pipelining and move tasks
** Directives

## v4.0.0 (12th Sep 17)

* Nucleus Studio 3.x.x support.

## v3.0.0 (5th Jul 17)

* Switches to .net 4.6.2 framework.

## v2.0.0 (30th May 17)

* Re-factor to support the service operation result pattern.

## v1.3.0 (26th Apr 17)

* Adds Agent service.

## v1.2.0 (18th Apr 17)

* Adds job state service.

## v1.0.0 (17th Mar 17)

* Initial release.
** Roadmap service.
** Job service.
** Servicing service.
