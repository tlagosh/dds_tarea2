namespace EscobaServidor;

public class CartasEnMesa
{
    private List<Carta> _cartasEnMesa = new List<Carta>();

    public List<Carta> Cartas => _cartasEnMesa;
    
    
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

    public void Sacar(Carta carta)
    {
        _cartasEnMesa.Remove(carta);
    }

    public int CantidadCartas()
    {
        return _cartasEnMesa.Count;
    }

    private bool HayCartasEnMesa() => _cartasEnMesa.Any();

    public List<List<Carta>> ObtenerTodasLasJugadas(Carta carta)
    {
        List<Carta> cartasEnJuego = new List<Carta>(_cartasEnMesa);
        cartasEnJuego.Add(carta);
        List<List<Carta>> jugadas = new List<List<Carta>>();
        List<List<Carta>> gruposDeCartasIguales = new List<List<Carta>>();
        gruposDeCartasIguales = cartasEnJuego.GroupBy(p => p.valor).Where(g => g.Count() > 1).Select(g => g.ToList()).ToList();

        List<List<Carta>> gruposDeCartas = new List<List<Carta>>();
        if (gruposDeCartasIguales.Any())
        {
            foreach (List<Carta> grupo in gruposDeCartasIguales)
            {
                foreach (Carta cartaEnGrupo in grupo)
                {
                    List<Carta> grupoDeCartas = new List<Carta>();
                    grupoDeCartas.Add(cartaEnGrupo);
                    grupoDeCartas.AddRange(cartasEnJuego.Where(c => c.valor != cartaEnGrupo.valor));
                    gruposDeCartas.Add(grupoDeCartas);
                }
            }
            foreach (List<Carta> grupo in gruposDeCartas)
            {
                jugadas.AddRange(ObtenerJugadas(grupo));
            }
        }
        else
        {
            jugadas.AddRange(ObtenerJugadas(cartasEnJuego));
        }

        return jugadas;
    }

    public List<List<Carta>> ObtenerJugadas(List<Carta> cartasEnJuego)
    {
        int[] arr = cartasEnJuego.Select(carta => carta.valor).ToArray();
        int n = arr.Length;
        int sum = 15;

        SubSetSum subSetSum = new SubSetSum();

        List<List<int>> valoresSumasDe15 = subSetSum.GetSubSetSums(arr, n, sum);

        List<List<Carta>> cartasSumasDe15 = new List<List<Carta>>();
        for (int i = 0; i < valoresSumasDe15.Count; i++)
        {
            List<Carta> cartasSumaDe15 = new List<Carta>();
            for (int j = 0; j < valoresSumasDe15[i].Count; j++)
            {
                foreach (Carta cartaEnJuego in cartasEnJuego)
                {
                    if (cartaEnJuego.valor == valoresSumasDe15[i][j])
                    {
                        cartasSumaDe15.Add(cartaEnJuego);
                        break;
                    }
                }
            }
            cartasSumasDe15.Add(cartasSumaDe15);
        }

        return cartasSumasDe15;
    }
    
}