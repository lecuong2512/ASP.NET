using System.ComponentModel.DataAnnotations;
using Day6_LeVIetCuong.Data;
using Day6_LeVIetCuong.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Xunit;

namespace Day6_LeVIetCuong.Tests;

public class MovieModelTests
{
    private static Day6_LeVIetCuongContext CreateSqlServerModelContext()
    {
        var options = new DbContextOptionsBuilder<Day6_LeVIetCuongContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Day6ModelInspection;Trusted_Connection=True;")
            .Options;

        return new Day6_LeVIetCuongContext(options);
    }

    [Fact]
    public void Model_ContainsOnlyGenreAndMovieWithSingularTableNames()
    {
        using var context = CreateSqlServerModelContext();
        var entityTypes = context.Model.GetEntityTypes().ToList();

        Assert.Equal(2, entityTypes.Count);

        var genreEntity = context.Model.FindEntityType(typeof(Genre));
        var movieEntity = context.Model.FindEntityType(typeof(Movie));

        Assert.NotNull(genreEntity);
        Assert.NotNull(movieEntity);
        Assert.Equal("Genre", genreEntity.GetTableName());
        Assert.Equal("Movie", movieEntity.GetTableName());
    }

    [Fact]
    public void Movie_GenreForeignKey_IsRequiredAndRestricted()
    {
        using var context = CreateSqlServerModelContext();
        var genreEntity = context.Model.FindEntityType(typeof(Genre))!;
        var movieEntity = context.Model.FindEntityType(typeof(Movie))!;

        var foreignKey = Assert.Single(movieEntity.GetForeignKeys());
        Assert.Equal(genreEntity, foreignKey.PrincipalEntityType);
        Assert.Equal(nameof(Movie.GenreId), Assert.Single(foreignKey.Properties).Name);
        Assert.True(foreignKey.IsRequired);
        Assert.Equal(DeleteBehavior.Restrict, foreignKey.DeleteBehavior);
    }

    [Fact]
    public void Model_SeedsStarterGenresAndSampleMovies()
    {
        using var context = CreateSqlServerModelContext();
        var designTimeModel = context.GetService<IDesignTimeModel>().Model;
        var genreSeeds = designTimeModel.FindEntityType(typeof(Genre))!.GetSeedData().ToList();
        var movieSeeds = designTimeModel.FindEntityType(typeof(Movie))!.GetSeedData().ToList();

        Assert.Equal(10, genreSeeds.Count);
        Assert.Contains(genreSeeds, s => (int)s[nameof(Genre.Id)]! == 1 && (string)s[nameof(Genre.Name)]! == "Hành động");

        Assert.Equal(2, movieSeeds.Count);
        Assert.Contains(movieSeeds, s =>
            (string)s[nameof(Movie.Title)]! == "Phim 1" &&
            ((DateTime)s[nameof(Movie.ReleaseDate)]!).Year == 2025 &&
            (int)s[nameof(Movie.Duration)]! == 100);
        Assert.Contains(movieSeeds, s =>
            (string)s[nameof(Movie.Title)]! == "Phim 2" &&
            ((DateTime)s[nameof(Movie.ReleaseDate)]!).Year == 2026 &&
            (int)s[nameof(Movie.Duration)]! == 120);
    }

    [Fact]
    public void MovieValidation_RequiresValidFieldsAndPositiveDuration()
    {
        var invalidMovie = new Movie
        {
            Title = "",
            ReleaseDate = new DateTime(2025, 1, 1),
            Duration = 0,
            GenreId = 0
        };

        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(invalidMovie, new ValidationContext(invalidMovie), results, validateAllProperties: true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains(nameof(Movie.Title)));
        Assert.Contains(results, r => r.MemberNames.Contains(nameof(Movie.Duration)));
        Assert.Contains(results, r => r.MemberNames.Contains(nameof(Movie.GenreId)));
    }

    [Fact]
    public void MovieValidation_AcceptsValidMovie()
    {
        var validMovie = new Movie
        {
            Title = "Phim Test",
            ReleaseDate = new DateTime(2026, 6, 1),
            Duration = 115,
            GenreId = 1
        };

        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(validMovie, new ValidationContext(validMovie), results, validateAllProperties: true);

        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void GenreValidation_RequiresName()
    {
        var invalidGenre = new Genre { Name = "" };
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(invalidGenre, new ValidationContext(invalidGenre), results, validateAllProperties: true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains(nameof(Genre.Name)));
    }
}
