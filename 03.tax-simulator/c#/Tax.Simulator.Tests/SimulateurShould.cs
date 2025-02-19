using FluentAssertions;
using Tax.Simulator.Tests.Integration;
using Xunit;

namespace Tax.Simulator.Tests;

public class SimulateurShould
{
    [Fact(DisplayName = "Calcul imp�t c�libataire")]
    public void testCalculImpotCelibataire()
    {
        Simulateur.CalculerImpotsAnnuel("C�libataire", 2000m, 0m, 0).Should().Be(1515.25m);
    }

    [Fact(DisplayName = "Calcul imp�t c�libataire invalide")]
    public void testCalculImpotCelibataireInvalide()
    {
        
        FluentActions.Invoking(() => Simulateur.CalculerImpotsAnnuel("C�libataire", -2000m, 0m, 0)).Should().Throw<ArgumentException>().WithMessage("Les salaires doivent �tre positifs.");
        
    }

    [Fact(DisplayName = "Calcul imp�t Mari�/Pacs�")]
    public void testCalculImp�tMariePacse()
    {
        Simulateur.CalculerImpotsAnnuel("Mari�/Pacs�", 2500m, 2000m, 0).Should().Be(4043.90m);
    }

    [Fact(DisplayName = "Calcul imp�t Mari�/Pacs� invalide")]
    public void testCalculImp�tMariePacseInvalide()
    {

        FluentActions.Invoking(() => Simulateur.CalculerImpotsAnnuel("Mari�/Pacs�", -2500m, 2000m, 0)).Should().Throw<ArgumentException>().WithMessage("Les salaires doivent �tre positifs.");

    }

    [Fact(DisplayName = "Prise en compte des enfants dans le quotient familial ")]
    public void testQuotientFamilial()
    {
        Simulateur.CalculerImpotsAnnuel("Mari�/Pacs�", 3000m, 3000m, 3).Should().Be(3983.37m);
    }

    [Fact(DisplayName = "Prise en compte des enfants dans le quotient familial Invalide")]
    public void testQuotientFamilialInvalide()
    {
        FluentActions.Invoking(() => Simulateur.CalculerImpotsAnnuel("Mari�/Pacs�", 3000m, 3000m, -1)).Should().Throw<ArgumentException>().WithMessage("Le nombre d'enfants ne peut pas �tre n�gatif.");
    }

    [Fact(DisplayName = "Situation familiale invalide")]
    public void testSituationFamilialeInvalide()
    {
        FluentActions.Invoking(() => Simulateur.CalculerImpotsAnnuel("Divorc�", 3000m, 3000m, 1)).Should().Throw<ArgumentException>().WithMessage("Situation familiale invalide.");
    }
}