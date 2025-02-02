﻿// <copyright file="ExtendedTypes.Custom.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>
// <auto-generated>
//     This source code extends auto-generated code of a T4 template.
// </auto-generated>
#nullable enable

using System.Collections.Specialized;
using System.ComponentModel;
using Mapster;
using MUnique.OpenMU.Persistence.Json;

namespace MUnique.OpenMU.Persistence.EntityFramework.Model;

using System.ComponentModel.DataAnnotations.Schema;
using MUnique.OpenMU.AttributeSystem;

/// <summary>
/// Extended <see cref="PowerUpDefinitionValue"/> by the properties of <see cref="SimpleElement"/>, because they are a 1:1 relationship.
/// </summary>
internal partial class PowerUpDefinitionValue
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PowerUpDefinitionValue"/> class.
    /// </summary>
    public PowerUpDefinitionValue()
    {
        this.ConstantValue = new MUnique.OpenMU.AttributeSystem.SimpleElement();
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public float Value
    {
        get => this.ConstantValue!.Value;
        set => this.ConstantValue!.Value = value;
    }

    /// <summary>
    /// Gets the type of the aggregate.
    /// </summary>
    public AggregateType AggregateType
    {
        get => this.ConstantValue!.AggregateType;
        set => this.ConstantValue!.AggregateType = value;
    }

    /// <summary>
    /// Gets or sets the parent as boost identifier.
    /// </summary>
    /// <remarks>
    /// This is required, because <see cref="PowerUpDefinitionValue"/> is used as <see cref="PowerUpDefinition.Boost"/> and as <see cref="PowerUpDefinitionWithDuration.Duration"/>.
    /// The entity framework will not name the foreign keys in a proper way, so they would collide.
    /// </remarks>
    public Guid? ParentAsBoostId { get; set; }

    /// <summary>
    /// Gets or sets the parent as boost.
    /// </summary>
    /// <remarks>
    /// This is required, because <see cref="PowerUpDefinitionValue"/> is used as <see cref="PowerUpDefinition.Boost"/> and as <see cref="PowerUpDefinitionWithDuration.Duration"/>.
    /// The entity framework will not name the foreign keys in a proper way, so they would collide.
    /// </remarks>
    [InverseProperty(nameof(PowerUpDefinitionWithDuration.RawBoost))]
    [Browsable(false)]
    public PowerUpDefinitionWithDuration? ParentAsBoost { get; set; }

    /// <summary>
    /// Gets or sets the parent as duration identifier.
    /// </summary>
    /// <remarks>
    /// This is required, because <see cref="PowerUpDefinitionValue"/> is used as <see cref="PowerUpDefinition.Boost"/> and as <see cref="PowerUpDefinitionWithDuration.Duration"/>.
    /// The entity framework will not name the foreign keys in a proper way, so they would collide.
    /// </remarks>
    public Guid? ParentAsDurationId { get; set; }

    /// <summary>
    /// Gets or sets the duration of the parent as.
    /// </summary>
    /// <remarks>
    /// This is required, because <see cref="PowerUpDefinitionValue"/> is used as <see cref="PowerUpDefinition.Boost"/> and as <see cref="PowerUpDefinitionWithDuration.Duration"/>.
    /// The entity framework will not name the foreign keys in a proper way, so they would collide.
    /// </remarks>
    [InverseProperty(nameof(PowerUpDefinitionWithDuration.RawDuration))]
    [Browsable(false)]
    public PowerUpDefinitionWithDuration? ParentAsDuration { get; set; }
}

/// <summary>
/// The Entity Framework Core implementation of <see cref="ConstValueAttribute"/>.
/// </summary>
internal partial class ConstValueAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstValueAttribute"/> class.
    /// </summary>
    public ConstValueAttribute()
    {
    }

    /// <summary>
    /// Gets or sets the character class identifier.
    /// </summary>
    /// <value>
    /// The character class identifier.
    /// </value>
    public Guid CharacterClassId { get; set; }

    /// <summary>
    /// Gets or sets the character class.
    /// </summary>
    /// <value>
    /// The character class.
    /// </value>
    public CharacterClass? CharacterClass { get; set; }

    /// <inheritdoc />
    public new float Value
    {
        get => base.Value;
        set => base.Value = value;
    }
}

/// <summary>
/// The Entity Framework Core implementation of <see cref="MUnique.OpenMU.DataModel.Entities.Item"/>.
/// </summary>
internal partial class Item
{
    [NotMapped]
    private ItemStorage? _itemStorage;

    public Guid? ItemStorageId { get; set; }

    [ForeignKey("ItemStorageId")]
    public ItemStorage? RawItemStorage
    {
        get => this._itemStorage;
        set
        {
            this._itemStorage = value;
            this.ItemStorageId = value?.Id;
        }
    }

    /// <summary>
    /// Clones the item option link.
    /// </summary>
    /// <param name="link">The link.</param>
    /// <returns>The cloned item option link.</returns>
    /// <remarks>It does not need to be explicitly added to the context, because it will happen automatically when the context detects the changes of the item.</remarks>
    protected override DataModel.Entities.ItemOptionLink CloneItemOptionLink(DataModel.Entities.ItemOptionLink link)
    {
        var persistentLink = new ItemOptionLink();
        persistentLink.AssignValues(link);
        return persistentLink;
    }
}

internal partial class ItemStorage
{
    public ItemStorage()
    {
        var notifier = this.Items as INotifyCollectionChanged;
        if (notifier != null)
        {
            notifier.CollectionChanged += this.OnItemsChanged;
        }
    }

    private void OnItemsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (e.NewItems is null)
                {
                    break;
                }

                foreach (Item item in e.NewItems)
                {
                    item.RawItemStorage = this;
                }

                break;
            case NotifyCollectionChangedAction.Reset:
            case NotifyCollectionChangedAction.Remove:
                if (e.OldItems is null)
                {
                    break;
                }

                foreach (Item item in e.OldItems)
                {
                    item.RawItemStorage = null;
                }

                break;
            default:
                // do nothing.
                break;
        }
    }
}

/// <summary>
/// The Entity Framework Core implementation of <see cref="MUnique.OpenMU.DataModel.Entities.GuildMember"/>.
/// </summary>
internal partial class GuildMember
{
    /// <summary>
    /// Gets or sets the character. This property just exists to define the foreign key.
    /// </summary>
    [ForeignKey(nameof(Id))]
    public Character Character { get; set; }
}

internal partial class Account : IConvertibleTo<BasicModel.Account>
{
    public BasicModel.Account Convert()
    {
        MapsterConfigurator.EnsureConfigured();

        return this.Adapt<BasicModel.Account>();
    }
}

internal partial class GameConfiguration : IConvertibleTo<BasicModel.GameConfiguration>
{
    public BasicModel.GameConfiguration Convert()
    {
        MapsterConfigurator.EnsureConfigured();

        return this.Adapt<BasicModel.GameConfiguration>();
    }
}

internal partial class ConnectServerDefinition : IConvertibleTo<BasicModel.ConnectServerDefinition>
{
    public BasicModel.ConnectServerDefinition Convert()
    {
        MapsterConfigurator.EnsureConfigured();
        return this.Adapt<BasicModel.ConnectServerDefinition>();
    }
}

internal partial class GameClientDefinition : IConvertibleTo<BasicModel.GameClientDefinition>
{
    public BasicModel.GameClientDefinition Convert()
    {
        MapsterConfigurator.EnsureConfigured();
        return this.Adapt<BasicModel.GameClientDefinition>();
    }
}

/// <summary>
/// The Entity Framework Core implementation of <see cref="MUnique.OpenMU.Interfaces.LetterHeader"/>.
/// This implementation adds the receiver additionally as reference to <see cref="Character"/>.
/// </summary>
/// <remarks>
/// You may ask, why we didn't add the sender as reference to <see cref="Character"/>. That's because
/// it's okay to keep just the name. Also, if the sender gets deleted, we still have the name and one reference less to care about.
/// </remarks>
internal partial class LetterHeader
{
    /// <summary>
    /// Gets or sets the receiver of the letter.
    /// </summary>
    [ForeignKey(nameof(ReceiverId))]
    public Character? Receiver { get; set; }

    /// <summary>
    /// Gets or sets the receiver identifier.
    /// </summary>
    public Guid ReceiverId { get; set; }
}