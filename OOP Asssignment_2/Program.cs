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
            public decimal EstimatedCost
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

        #region DeliveryCenter class
        public class DeliveryCenter
        {
            private Shipment[] shipments;
            private int count;
            public DeliveryCenter()
            {
                shipments = new Shipment[10];
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
                    shipments = new Shipment[10];
                }

                if (count >= 10)
                {
                    return false;
                }

                shipments[count] = shipment;
                count++;
                return true;
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
