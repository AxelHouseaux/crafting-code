using FluentAssertions;
using Xunit;

namespace Tax.Simulator.Tests;

public class SimulateurShould
{

    [Fact(DisplayName = "Calcul impôt célibataire")]
    public void testCalculImpotCelibataire()
    {
        Simulateur.CalculerImpotsAnnuel(SituationFamiliale.CELIBATAIRE, 2000m, 0m, 0).Should().Be(1515.25m);
    }
    
    [Fact(DisplayName = "Calcul impôt célibataire invalide")]
    public void testCalculImpotCelibataireInvalide()
    {
        
        FluentActions.Invoking(() => Simulateur.CalculerImpotsAnnuel(SituationFamiliale.CELIBATAIRE, -2000m, 0m, 0)).Should().Throw<ArgumentException>().WithMessage("Les salaires doivent être positifs.");
        FluentActions.Invoking(() => Simulateur.CalculerImpotsAnnuel(SituationFamiliale.CELIBATAIRE, 0m, 0m, 0)).Should().Throw<ArgumentException>().WithMessage("Les salaires doivent être positifs.");
    }

    
    [Fact(DisplayName = "Calcul impôt Marié/Pacsé")]
    public void testCalculImpotMariePacse()
    {
        Simulateur.CalculerImpotsAnnuel(SituationFamiliale.MARIE_PACSE, 2500m, 2000m, 0).Should().Be(4043.90m);
        Simulateur.CalculerImpotsAnnuel(SituationFamiliale.MARIE_PACSE, 2500m, 0, 0).Should().Be(1050.50m);
    }

    
    [Fact(DisplayName = "Calcul impôt Marié/Pacsé invalide")]
    public void testCalculImpotMariePacseInvalide()
    {

        FluentActions.Invoking(() => Simulateur.CalculerImpotsAnnuel(SituationFamiliale.MARIE_PACSE, 2000m, -2500m, 0)).Should().Throw<InvalidDataException>().WithMessage("Les salaires doivent être positifs.");

    }

    [Fact(DisplayName = "Prise en compte des enfants dans le quotient familial ")]
    public void testQuotientFamilial()
    {
        Simulateur.CalculerImpotsAnnuel(SituationFamiliale.MARIE_PACSE, 3000m, 3000m, 2).Should().Be(4545.75m);
        Simulateur.CalculerImpotsAnnuel(SituationFamiliale.MARIE_PACSE, 3000m, 3000m, 3).Should().Be(3983.37m);
        Simulateur.CalculerImpotsAnnuel(SituationFamiliale.MARIE_PACSE, 3000m, 3000m, 4).Should().Be(3421.00m);
    }

    [Fact(DisplayName = "Prise en compte des enfants dans le quotient familial Invalide")]
    public void testQuotientFamilialInvalide()
    {
        FluentActions.Invoking(() => Simulateur.CalculerImpotsAnnuel(SituationFamiliale.MARIE_PACSE, 3000m, 3000m, -1)).Should().Throw<ArgumentException>().WithMessage("Le nombre d'enfants ne peut pas être négatif.");
    }

    [Fact(DisplayName = "Situation familiale invalide")]
    public void testSituationFamilialeInvalide()
    {
        FluentActions.Invoking(() => Simulateur.CalculerImpotsAnnuel(SituationFamiliale.INCONNUE, 3000m, 3000m, 1)).Should().Throw<ArgumentException>().WithMessage("Situation familiale invalide.");
    }

    [Fact(DisplayName = "Tranches d'impôts")]
    public void testTranchesImpots()
    {
        Simulateur.CalculerImpotsAnnuel(SituationFamiliale.CELIBATAIRE, 850m, 0m, 0).Should().Be(0m);
        Simulateur.CalculerImpotsAnnuel(SituationFamiliale.CELIBATAIRE, 2000m, 0m, 0).Should().Be(1515.25m);
        Simulateur.CalculerImpotsAnnuel(SituationFamiliale.CELIBATAIRE, 3333m, 0m, 0).Should().Be(5920.75m);
        Simulateur.CalculerImpotsAnnuel(SituationFamiliale.CELIBATAIRE, 7500m, 0m, 0).Should().Be(22622.00m);
        Simulateur.CalculerImpotsAnnuel(SituationFamiliale.CELIBATAIRE, 16667m, 0m, 0).Should().Be(69310.36m);

        Simulateur.CalculerImpotsAnnuel(SituationFamiliale.CELIBATAIRE, 2172.5m, 0m, 0).Should().Be(1742.95m);
    }

    [Fact(DisplayName = "Tranche d'impôts haut revenus")]
    public void testTrancheImpotHautsRevenus()
    {
        Simulateur.CalculerImpotsAnnuel(SituationFamiliale.CELIBATAIRE, 45000m, 0m, 0).Should().Be(223_508.56m);
        Simulateur.CalculerImpotsAnnuel(SituationFamiliale.MARIE_PACSE, 25000, 30000, 2).Should().Be(234_925.68m);
    }
}