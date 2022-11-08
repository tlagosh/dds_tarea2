namespace EscobaServidor;

public class CartasEnMesa
{
    private List<Carta> _cartasEnMesa = new List<Carta>();
    
    
    public override string ToString()
    {
        string cartas = "";
        foreach (Carta carta in _cartasEnMesa)
            cartas += carta;
        return cartas;
    }
    
    public void Agregar(Carta carta)
    {
        _cartasEnMesa.Add(carta);
    }

    private bool HayCartasEnMesa() => _cartasEnMesa.Any();

    public (List<Carta>, bool) ObtenerCartasGanadas(Carta carta)
    {
        List<Carta> cartasGanadas = new List<Carta>();
        bool escoba = false;

        int[] arr = _cartasEnMesa.Select(carta => carta.valor).ToArray();
        arr.Append(carta.valor);
        int n = arr.Length;
        int sum = 15;

        SubSetSum subSetSum = new SubSetSum();

        subSetSum.GetSubSetSums(arr, n, sum);

        return (cartasGanadas, escoba);
    }
    
}