namespace OOP_Asssignment_2
{
    internal class Program
    {
        #region Functions From Assignment 1 (Edited Part 2 Q1)
        public struct DeliveryAddress
        {
            public string City;
            public string Street;
            public int BuildingNumber;

            public DeliveryAddress(string city, string street, int buildingNumber)
            {
                City = city;
                Street = street;
                BuildingNumber = buildingNumber;
            }

            public string GetFullAddress()
            {
                return $"{BuildingNumber} {Street}, {City}";
            }
        }

        #region Ceate a Shipment class Part2
        public class Shipment
        {
            private string trackingCode;
            private string description;
            private double weight;
            private decimal deliveryFee;

            public Shipment(string trackingCode, string description, double weight, decimal deliveryFee, DeliveryAddress destination)
            {

                this.trackingCode = string.IsNullOrWhiteSpace(trackingCode) ? "UNVALID" : trackingCode;
                this.description = string.IsNullOrWhiteSpace(description) ? "NVALID" : description;
                this.weight = weight > 0 ? weight : 1.0;
                this.deliveryFee = deliveryFee > 0 ? deliveryFee : 10.0m;
                Destination = destination;
            }

            public Shipment(string trackingCode)
            : this(trackingCode, "Unknown", 1.0, 50.0m, new DeliveryAddress("Default City", "Default St", 1))
            {
            }

            public string TrackingCode
            {
                get { return trackingCode; }
                private set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        trackingCode = value;
                    }
                }
            }

            public string Description
            {
                get { return description; }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        description = value;
                    }
                }
            }

            public double Weight
            {
                get { return weight; }
                set
                {
                    if (value > 0)
                    {
                        weight = value;
                    }
                }
            }

            public decimal DeliveryFee
            {
                get { return deliveryFee; }
                private set
                {
                    if (value > 0)
                    {
                        deliveryFee = value;
                    }
                }
            }

            public DeliveryAddress Destination { get; set; }
            public virtual decimal EstimatedCost
            {
                get
                {
                    return deliveryFee + ((decimal)weight * 5m);
                }
            }

            public void UpdateDeliveryFee(decimal newFee)
            {
                if (newFee > 0)
                {
                    deliveryFee = newFee;
                }
            }
            public void PrintShipment()
            {
                Console.WriteLine($"Tracking Code: {TrackingCode}");
                Console.WriteLine($"Description: {Description}");
                Console.WriteLine($"Weight: {Weight}");
                Console.WriteLine($"Delivery Fee: {DeliveryFee}");
                Console.WriteLine($"Destination: {Destination.GetFullAddress()}");
                Console.WriteLine($"Estimated Cost: {EstimatedCost}");
                Console.WriteLine(new string('-', 30));
            }
        }
        #endregion

        #region DeliveryCenter class (Edited)
        public class DeliveryCenter
        {
            private string centerName;
            private Shipment[] shipments;
            private int count;
            public DeliveryCenter(string centerName = "Main Center")
            {
                this.centerName = centerName;
                shipments = new Shipment[20]; 
                count = 0;
            }

            public Shipment this[int index]
            {
                get
                {
                    if (shipments == null || index < 0 || index >= count)
                    {
                        return null;
                    }
                    return shipments[index];
                }
                set
                {
                    if (shipments != null && index >= 0 && index < count)
                    {
                        shipments[index] = value;
                    }
                }
            }


            public Shipment this[string trackingCode]
            {
                get
                {
                    if (shipments == null || string.IsNullOrWhiteSpace(trackingCode))
                    {
                        return null;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        if (shipments[i].TrackingCode != null &&
                            shipments[i].TrackingCode.Equals(trackingCode, StringComparison.OrdinalIgnoreCase))
                        {
                            return shipments[i];
                        }
                    }

                    return null;
                }
            }

            public bool AddShipment(Shipment shipment)
            {
                if (shipments == null)
                {
                    shipments = new Shipment[20];
                }

                if (count >= 20 || shipment == null)
                {
                    return false;
                }

                shipments[count] = shipment;
                count++;
                return true;
            }
            public bool RemoveShipment(string trackingCode)
            {
                if (string.IsNullOrWhiteSpace(trackingCode) || count == 0)
                {
                    return false;
                }

                int indexToRemove = -1;
                for (int i = 0; i < count; i++)
                {
                    if (shipments[i]?.TrackingCode != null &&
                        shipments[i].TrackingCode.Equals(trackingCode, StringComparison.OrdinalIgnoreCase))
                    {
                        indexToRemove = i;
                        break;
                    }
                }
                if (indexToRemove == -1)
                {
                    return false;
                }
                for (int i = indexToRemove; i < count - 1; i++)
                {
                    shipments[i] = shipments[i + 1];
                }
                shipments[count - 1] = null;
                count--;

                return true;
            }
            public void PrintAllShipments()
            {
                Console.WriteLine($"--- Delivery Center: {centerName} ---");
                Console.WriteLine($"Total Shipments: {count} / 20");
                Console.WriteLine(new string('=', 30));

                if (count == 0)
                {
                    Console.WriteLine("No shipments available in this center.");
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Shipment {i + 1}:");
                        shipments[i].PrintShipment(); 
                    }
                }
            }
        }
        #endregion
        #endregion

        #region StandardShipment Class
        public class StandardShipment : Shipment
        {
            public StandardShipment(string trackingCode, string description, double weight, decimal deliveryFee, DeliveryAddress destination)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
            }

            public StandardShipment(string trackingCode)
                : base(trackingCode)
            {
            }
        }
        #endregion

        #region ExpressShipment Class
        public class ExpressShipment : Shipment
        {
            private decimal extraFee;

            public decimal ExtraFee
            {
                get { return extraFee; }
                set
                {
                    if (value >= 0)
                    {
                        extraFee = value;
                    }
                }
            }

            public ExpressShipment(string trackingCode, string description, double weight, decimal deliveryFee, DeliveryAddress destination, decimal extraFee)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
                ExtraFee = extraFee >= 0 ? extraFee : 0m;
            }
            public override decimal EstimatedCost
            {
                get
                {
                    return base.EstimatedCost + ExtraFee;
                }
            }
        }
        #endregion

        #region InternationalShipment Class
        public class InternationalShipment : Shipment
        {
            private string destinationCountry;
            private decimal customsFee;

            public string DestinationCountry
            {
                get { return destinationCountry; }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        destinationCountry = value;
                    }
                }
            }

            public decimal CustomsFee
            {
                get { return customsFee; }
                set
                {
                    if (value >= 0)
                    {
                        customsFee = value;
                    }
                }
            }

            public InternationalShipment(string trackingCode, string description, double weight, decimal deliveryFee, DeliveryAddress destination, string destinationCountry, decimal customsFee)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
                DestinationCountry = string.IsNullOrWhiteSpace(destinationCountry) ? "Unknown" : destinationCountry;
                CustomsFee = customsFee >= 0 ? customsFee : 0m;
            }

            public override decimal EstimatedCost
            {
                get
                {
                    return base.EstimatedCost + CustomsFee;
                }
            }
        }
        #endregion
        static void Main(string[] args)
        {
            #region Question 1
            // What is the difference between a class and a struct?
            // A class is a reference type, while a struct is a value type. 
            // Classes are allocated on the heap, while structs are allocated on the stack.
            // classes are private by default, while structs are public by default.

            // Why are classes more suitable than structs for large applications?
            // Because they support inheritance, polymorphism, and encapsulation
            #endregion


            #region Question 2
            // a) shipment
            // b) ExpressShipment
            // c) TrackingCode
            // d) Inheritance allows for code reusability and reduces redundancy, making the codebase easier to maintain and extend.
            #endregion



        }
    }
}
