namespace EscobaServidor;

public class PilaCartas
{
    private List<Carta> _cartas;

    public PilaCartas()
    {
        GenerarCartas();
    }

    private void GenerarCartas()
    {
        _cartas = new List<Carta>();
        string[] pintas = { "Oro", "Basto", "Espada", "Copa" };
        for(int i = 1; i < 8; i++)
            foreach (string pinta in pintas)
                _cartas.Add(new Carta(i, pinta));
    }

    public void DarCartas(Jugador jugador, int cantidadCartas)
    {
        for (int i = 0; i < cantidadCartas; i++)
            jugador.AgregarCartaAMano(SacarCartaAlAzar());
    }

    public Carta SacarCartaAlAzar()
    {
        int idCarta = GeneradorNumerosAleatorios.Generar(_cartas.Count - 1);
        Carta cartaSacada = _cartas[idCarta];
        _cartas.Remove(cartaSacada);
        return cartaSacada;
    }

    public override string ToString()
    {
        string s = "";
        foreach (var carta in _cartas)
            s += carta;
        return s;
    }

    public bool TieneCartas() => _cartas.Any();
}