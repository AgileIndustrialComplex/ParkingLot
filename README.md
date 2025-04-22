# Parking Lot

## Notes
1. given code has been written with extensibility in mind first
2. most recent version of dotnet has been used (as it is what I'm most familiar with)
3. logic, models and interfaces has been split into separate projects (standard practice in dotnet, models can be emitted as a package if needed)
4. most time I've spend prototyping
5. the required functionality can be accessed through `IParkingLot` interface

## Issues
1. selecting a free spot requires iterating through all spots in the lot, with current implementation can be alleviated by saving the state of iterator
2. the implementation is not thread safe
