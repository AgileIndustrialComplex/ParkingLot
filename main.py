CAR_TYPE = "car"
MOTORCYCLE_TYPE = "motorcycle"

class ParkingSpot:
    def __init__(self, type):
        self.type = type
        self.vehicle = None

class ParkingLot:
    def __init__(self, parking):
        self.__parking = parking

    def park(self, vehicle):
        for row in self.__parking:
            for spot in row:
                if spot.vehicle is not None:
                    continue
                if vehicle.type == MOTORCYCLE_TYPE:  # motorcycle can park everywhere
                    spot.vehicle = vehicle
                    return spot
                if vehicle.type == CAR_TYPE and spot.type == CAR_TYPE:
                    spot.vehicle = vehicle
                    return spot
                
        raise Exception("Could not find a free parking spot")


    def unpark(self, vehicle):
        for row in self.__parking:
            for spot in row:
                if spot.vehicle == vehicle:
                    spot.vehicle = None
                    return
                    
        raise Exception("Given vehicle is not parked in this lot")
        

    def find_parked(self, spot):
        for row in self.__parking:
            for rowSpot in row:
                if rowSpot == spot:
                    return spot.vehicle

        raise Exception("Given spot is not a part of the parking lot")

class Vehicle:
    def __init__(self, type):
        self.type = type

parking = [
    [
        ParkingSpot(CAR_TYPE),
        ParkingSpot(CAR_TYPE),
    ],
    [
        ParkingSpot(MOTORCYCLE_TYPE),
    ],
]
lot = ParkingLot(parking)

# park
car = Vehicle(CAR_TYPE)
moto = Vehicle(MOTORCYCLE_TYPE)
spot_car = lot.park(car)

spot_moto = lot.park(moto)
print(spot_car.type, spot_car.vehicle)
print(spot_moto.type, spot_moto.vehicle)

try:
    car2 = Vehicle(CAR_TYPE)
    lot.park(car2)
except Exception as e:
    print(f"{e}")

moto2 = Vehicle(MOTORCYCLE_TYPE)
moto2_spot = lot.park(moto2)

# unpark
lot.unpark(car)
try:
    lot.unpark(car)
except Exception as e:
    print(e)

try:
    lot.unpark(Vehicle(MOTORCYCLE_TYPE))
except Exception as e:
    print(e)

# find parked
found_vehicle = lot.find_parked(spot_moto)
print(found_vehicle, found_vehicle.type)

try:
    lot.find_parked(ParkingSpot(CAR_TYPE))
except Exception as e:
    print(e)

# Provide code for a parking lot with the following assumptions,
# 
# • The parking lot has multiple levels. Each level has multiple rows of spots. 
# • The parking lot has motorcycle spots and car spots.
# • A motorcycle can park in any empty spot that is empty. 
# • A car can only park in a single car spot that is empty. 
#
# Provide 3 functions for a working parking lot:
# Given a vehicle, you should be able to park it.
# Given a vehicle, you should be able to unpark it.
# Given a spot, you should be able to find the vehicle parked in it.