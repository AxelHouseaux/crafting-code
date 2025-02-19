using FluentAssertions;
using Tax.Simulator.Tests.Integration;
using Xunit;

namespace Tax.Simulator.Tests;

public class SimulateurShould
{
    [Fact(DisplayName = "Calcul impôt célibataire")]
    public void testCalculImpotCelibataire()
    {
        Simulateur.CalculerImpotsAnnuel("Célibataire", 2000m, 0m, 0).Should().Be(1515.25m);
    }

    [Fact(DisplayName = "Calcul impôt célibataire invalide")]
    public void testCalculImpotCelibataireInvalide()
    {
        
        FluentActions.Invoking(() => Simulateur.CalculerImpotsAnnuel("Célibataire", -2000m, 0m, 0)).Should().Throw<ArgumentException>().WithMessage("Les salaires doivent être positifs.");
        
    }

    [Fact(DisplayName = "Calcul impôt Marié/Pacsé")]
    public void testCalculImpôtMariePacse()
    {
        Simulateur.CalculerImpotsAnnuel("Marié/Pacsé", 2500m, 2000m, 0).Should().Be(4043.90m);
    }

    [Fact(DisplayName = "Calcul impôt Marié/Pacsé invalide")]
    public void testCalculImpôtMariePacseInvalide()
    {

        FluentActions.Invoking(() => Simulateur.CalculerImpotsAnnuel("Marié/Pacsé", -2500m, 2000m, 0)).Should().Throw<ArgumentException>().WithMessage("Les salaires doivent être positifs.");

    }

    [Fact(DisplayName = "Prise en compte des enfants dans le quotient familial ")]
    public void testQuotientFamilial()
    {
        Simulateur.CalculerImpotsAnnuel("Marié/Pacsé", 3000m, 3000m, 3).Should().Be(3983.37m);
    }

    [Fact(DisplayName = "Prise en compte des enfants dans le quotient familial Invalide")]
    public void testQuotientFamilialInvalide()
    {
        FluentActions.Invoking(() => Simulateur.CalculerImpotsAnnuel("Marié/Pacsé", 3000m, 3000m, -1)).Should().Throw<ArgumentException>().WithMessage("Le nombre d'enfants ne peut pas être négatif.");
    }

    [Fact(DisplayName = "Situation familiale invalide")]
    public void testSituationFamilialeInvalide()
    {
        FluentActions.Invoking(() => Simulateur.CalculerImpotsAnnuel("Divorcé", 3000m, 3000m, 1)).Should().Throw<ArgumentException>().WithMessage("Situation familiale invalide.");
    }
}