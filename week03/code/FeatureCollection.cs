public class FeatureCollection
{
    public Features[] Features { get; set; }
}
public class Features
{
    public Properties Properties { get; set; }

}
public class Properties
{
    public decimal Mag { get; set; }
    public string Place { get; set; }

}