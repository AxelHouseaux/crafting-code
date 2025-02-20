namespace Tax.Simulator;

public static class Simulateur
{
    #region attributs privés
    private static readonly decimal[] TranchesImposition = {10225m, 26070m, 74545m, 160336m}; // Plafonds des tranches
    private static readonly decimal[] TauxImposition = {0.0m, 0.11m, 0.30m, 0.41m, 0.45m}; // Taux correspondants
    #endregion

    #region méthodes privées
    private static void GestionErreurs(SituationFamiliale situationFamiliale, int nombreEnfants, decimal salaireMensuel, decimal salaireMensuelConjoint)
    {
        if (situationFamiliale != SituationFamiliale.CELIBATAIRE && situationFamiliale != SituationFamiliale.MARIE_PACSE)
        {
            throw new ArgumentException("Situation familiale invalide.");
        }

        if (salaireMensuel <= 0)
        {
            throw new ArgumentException("Les salaires doivent être positifs.");
        }

        if (situationFamiliale == SituationFamiliale.MARIE_PACSE && salaireMensuelConjoint < 0)
        {
            throw new InvalidDataException("Les salaires doivent être positifs.");
        }

        if (nombreEnfants < 0)
        {
            throw new ArgumentException("Le nombre d'enfants ne peut pas être négatif.");
        }
    }

    private static decimal CalculerQuotientEnfants(int nombreEnfants)
    {
        decimal quotientEnfants;

        if(nombreEnfants <= 2)
        {
            quotientEnfants = 0.5m * nombreEnfants;
        }
        else
        {
            quotientEnfants = 1.0m + (nombreEnfants - 2) * 0.5m;
        }

        return quotientEnfants;
    }

    private static decimal CalculerRevenuAnnuel(
        SituationFamiliale situationFamiliale,
        decimal salaireMensuel,
        decimal salaireMensuelConjoint = 0m)
    {
        decimal revenuAnnuel;
        if (situationFamiliale == SituationFamiliale.MARIE_PACSE)
        {
            revenuAnnuel = (salaireMensuel + salaireMensuelConjoint) * 12;
        }
        else
        {
            revenuAnnuel = salaireMensuel * 12;
        }

        return revenuAnnuel;
    }

    private static decimal CalculerImpot(decimal revenuImposableParPart)
    {
        decimal impot = 0;
        for (var i = 0; i < TranchesImposition.Length; i++)
        {
            if (revenuImposableParPart <= TranchesImposition[i])
            {
                impot += (revenuImposableParPart - (i > 0 ? TranchesImposition[i - 1] : 0)) * TauxImposition[i];
                break;
            }
            else
            {
                impot += (TranchesImposition[i] - (i > 0 ? TranchesImposition[i - 1] : 0)) * TauxImposition[i];
            }
        }

        if (revenuImposableParPart > TranchesImposition[^1])
        {
            impot += (revenuImposableParPart - TranchesImposition[^1]) * TauxImposition[^1];
        }
        return impot;
    }

    private static decimal CalculerPartsFiscales(SituationFamiliale situationFamiliale, int nombreEnfants)
    {
        var baseQuotient = situationFamiliale == SituationFamiliale.MARIE_PACSE ? 2 : 1;

        decimal quotientEnfants = CalculerQuotientEnfants(nombreEnfants);

        var partsFiscales = baseQuotient + quotientEnfants;
        return partsFiscales;
    }
    #endregion

    #region méthodes publiques
    public static decimal CalculerImpotsAnnuel(
        SituationFamiliale situationFamiliale,
        decimal salaireMensuel,
        decimal salaireMensuelConjoint,
        int nombreEnfants)
    {
        GestionErreurs(situationFamiliale, nombreEnfants, salaireMensuel, salaireMensuelConjoint);

        decimal revenuAnnuel = CalculerRevenuAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint);

        decimal partsFiscales = CalculerPartsFiscales(situationFamiliale, nombreEnfants);

        decimal revenuImposableParPart = revenuAnnuel / partsFiscales;

        decimal impot = CalculerImpot(revenuImposableParPart);

        return Math.Round(impot * partsFiscales, 2);
    }
    #endregion
}