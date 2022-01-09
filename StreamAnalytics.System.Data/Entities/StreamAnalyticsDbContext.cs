using Microsoft.EntityFrameworkCore;

namespace StreamAnalytics.System.Data.Entities
{
  public partial class StreamAnalyticsDbContext : DbContext
  {
    public StreamAnalyticsDbContext()
    {
    }

    public StreamAnalyticsDbContext(DbContextOptions<StreamAnalyticsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attribute> Attributes { get; set; } = null!;
    public virtual DbSet<AttributeValue> AttributeValues { get; set; } = null!;
    public virtual DbSet<AuditLog> AuditLogs { get; set; } = null!;
    public virtual DbSet<BridgeAttribute> BridgeAttributes { get; set; } = null!;
    public virtual DbSet<DataType> DataTypes { get; set; } = null!;
    public virtual DbSet<DeletedResourceTask> DeletedResourceTasks { get; set; } = null!;
    public virtual DbSet<Enterprise> Enterprises { get; set; } = null!;
    public virtual DbSet<EnterpriseTenantActive> EnterpriseTenantActives { get; set; } = null!;
    public virtual DbSet<EventReason> EventReasons { get; set; } = null!;
    public virtual DbSet<EventType> EventTypes { get; set; } = null!;
    public virtual DbSet<EventWorkRequest> EventWorkRequests { get; set; } = null!;
    public virtual DbSet<IoTGateway> IoTGateways { get; set; } = null!;
    public virtual DbSet<MasterStreamType> MasterStreamTypes { get; set; } = null!;
    public virtual DbSet<MeasurementUnit> MeasurementUnits { get; set; } = null!;
    public virtual DbSet<OpcTag> OpcTags { get; set; } = null!;
    public virtual DbSet<OpcTagEvent> OpcTagEvents { get; set; } = null!;
    public virtual DbSet<OpcTagStream> OpcTagStreams { get; set; } = null!;
    public virtual DbSet<OpcTagThreshold> OpcTagThresholds { get; set; } = null!;
    public virtual DbSet<Permission> Permissions { get; set; } = null!;
    public virtual DbSet<PhysicalAsset> PhysicalAssets { get; set; } = null!;
    public virtual DbSet<PhysicalAssetAttribute> PhysicalAssetAttributes { get; set; } = null!;
    public virtual DbSet<PhysicalAssetBridgeAttribute> PhysicalAssetBridgeAttributes { get; set; } = null!;
    public virtual DbSet<PhysicalAssetIoTGateway> PhysicalAssetIoTGateways { get; set; } = null!;
    public virtual DbSet<PhysicalAssetIoTGatewayReportedStatusR1> PhysicalAssetIoTGatewayReportedStatusR1s { get; set; } = null!;
    public virtual DbSet<PhysicalAssetOpcStatusConfigR1> PhysicalAssetOpcStatusConfigR1s { get; set; } = null!;
    public virtual DbSet<PhysicalAssetOpcStatusPayloadR1> PhysicalAssetOpcStatusPayloadR1s { get; set; } = null!;
    public virtual DbSet<ResourceAuditLog> ResourceAuditLogs { get; set; } = null!;
    public virtual DbSet<Role> Roles { get; set; } = null!;
    public virtual DbSet<SchemaVersion> SchemaVersions { get; set; } = null!;
    public virtual DbSet<StreamType> StreamTypes { get; set; } = null!;
    public virtual DbSet<SystemInformationBuildDatum> SystemInformationBuildData { get; set; } = null!;
    public virtual DbSet<TagEventDatum> TagEventData { get; set; } = null!;
    public virtual DbSet<Tenant> Tenants { get; set; } = null!;
    public virtual DbSet<UserAccount> UserAccounts { get; set; } = null!;
    public virtual DbSet<UserAccountSetting> UserAccountSettings { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured) {
        optionsBuilder.UseSqlServer("Server=localhost; Database=Connected_Manufacturing; User Id=sa;Password=Wel@plex30");
        //optionsBuilder.LogTo(Console.WriteLine);
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Attribute>(entity => {
        entity.HasKey(e => new { e.TenantId, e.AttributeId });

        entity.ToTable("Attribute");

        entity.HasIndex(e => new { e.TenantId, e.AttributeName }, "IX_Tenant_Id_Attribute_Name")
            .IsUnique();

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.AttributeId).HasColumnName("Attribute_Id");

        entity.Property(e => e.AttributeName)
            .HasMaxLength(255)
            .HasColumnName("Attribute_Name");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.Attributes)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Attribute_Tenant");
      });

      modelBuilder.Entity<AttributeValue>(entity => {
        entity.HasKey(e => new { e.TenantId, e.AttributeValueId });

        entity.ToTable("Attribute_Value");

        entity.HasIndex(e => new { e.TenantId, e.AttributeId, e.AttributeValue1 }, "IX_Tenant_Id_Attribute_Id_Attribute_Value")
            .IsUnique();

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.AttributeValueId).HasColumnName("Attribute_Value_Id");

        entity.Property(e => e.AttributeId).HasColumnName("Attribute_Id");

        entity.Property(e => e.AttributeValue1)
            .HasMaxLength(255)
            .HasColumnName("Attribute_Value");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.AttributeValues)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Attribute_Value_Tenant");

        entity.HasOne(d => d.Attribute)
            .WithMany(p => p.AttributeValues)
            .HasForeignKey(d => new { d.TenantId, d.AttributeId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Attribute_Value_Attribute");
      });

      modelBuilder.Entity<AuditLog>(entity => {
        entity.HasKey(e => new { e.TenantId, e.AuditLogKey });

        entity.ToTable("Audit_Log");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.AuditLogKey)
            .ValueGeneratedOnAdd()
            .HasColumnName("AuditLog_Key");

        entity.Property(e => e.AccountId).HasColumnName("Account_Id");

        entity.Property(e => e.ActionName)
            .HasMaxLength(255)
            .HasColumnName("Action_Name");

        entity.Property(e => e.CreatedTimeStamp).HasColumnName("Created_TimeStamp");

        entity.Property(e => e.EntityName)
            .HasMaxLength(255)
            .HasColumnName("Entity_Name");

        entity.Property(e => e.NewEntity).HasColumnName("New_Entity");

        entity.Property(e => e.OldEntity).HasColumnName("Old_Entity");
      });

      modelBuilder.Entity<BridgeAttribute>(entity => {
        entity.ToTable("Bridge_Attribute");

        entity.Property(e => e.BridgeAttributeId)
            .ValueGeneratedNever()
            .HasColumnName("Bridge_Attribute_Id");

        entity.Property(e => e.BridgeAttributeName)
            .HasMaxLength(255)
            .HasColumnName("Bridge_Attribute_Name");
      });

      modelBuilder.Entity<DataType>(entity => {
        entity.ToTable("Data_Type");

        entity.Property(e => e.DataTypeId)
            .ValueGeneratedNever()
            .HasColumnName("Data_Type_Id");

        entity.Property(e => e.DataTypeName)
            .HasMaxLength(255)
            .HasColumnName("Data_Type_Name");
      });

      modelBuilder.Entity<DeletedResourceTask>(entity => {
        entity.HasKey(e => new { e.TenantId, e.IndexingTask })
            .HasName("PK_Deleted_Resource");

        entity.ToTable("Deleted_Resource_Task");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.IndexingTask)
            .HasMaxLength(255)
            .HasColumnName("Indexing_Task");

        entity.Property(e => e.DeletedId).HasColumnName("Deleted_Id");

        entity.Property(e => e.IdType)
            .HasMaxLength(255)
            .HasColumnName("Id_Type");

        entity.Property(e => e.SubmittedTimestamp).HasColumnName("Submitted_Timestamp");
      });

      modelBuilder.Entity<Enterprise>(entity => {
        entity.ToTable("Enterprise");

        entity.Property(e => e.EnterpriseId)
            .ValueGeneratedNever()
            .HasColumnName("Enterprise_Id");

        entity.Property(e => e.Active)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.EnterpriseName)
            .HasMaxLength(255)
            .HasColumnName("Enterprise_Name");
      });

      modelBuilder.Entity<EnterpriseTenantActive>(entity => {
        entity.HasNoKey();

        entity.ToView("Enterprise_Tenant_Active");

        entity.Property(e => e.EnterpriseId).HasColumnName("Enterprise_Id");

        entity.Property(e => e.EnterpriseName)
            .HasMaxLength(255)
            .HasColumnName("Enterprise_Name");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.TenantName)
            .HasMaxLength(255)
            .HasColumnName("Tenant_Name");
      });

      modelBuilder.Entity<EventReason>(entity => {
        entity.HasKey(e => new { e.TenantId, e.EventReasonId });

        entity.ToTable("Event_Reason");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.EventReasonId).HasColumnName("Event_Reason_Id");

        entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

        entity.Property(e => e.CreatedTimestamp).HasColumnName("Created_Timestamp");

        entity.Property(e => e.EventReasonName)
            .HasMaxLength(255)
            .HasColumnName("Event_Reason_Name");

        entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

        entity.Property(e => e.UpdatedTimestamp).HasColumnName("Updated_Timestamp");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.EventReasons)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Event_Reason_Tenant");
      });

      modelBuilder.Entity<EventType>(entity => {
        entity.ToTable("Event_Type");

        entity.Property(e => e.EventTypeId)
            .ValueGeneratedNever()
            .HasColumnName("Event_Type_Id");

        entity.Property(e => e.EventTypeName)
            .HasMaxLength(255)
            .HasColumnName("Event_Type_Name");
      });

      modelBuilder.Entity<EventWorkRequest>(entity => {
        entity.HasKey(e => new { e.TenantId, e.EventId });

        entity.ToTable("Event_Work_Request");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.EventId).HasColumnName("Event_Id");

        entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

        entity.Property(e => e.CreatedTimestamp).HasColumnName("Created_Timestamp");

        entity.Property(e => e.WorkRequestNo)
            .HasMaxLength(50)
            .HasColumnName("Work_Request_No");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.EventWorkRequests)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Event_Work_Request_Tenant");
      });

      modelBuilder.Entity<IoTGateway>(entity => {
        entity.HasKey(e => new { e.TenantId, e.IoTGatewayId });

        entity.ToTable("IoT_Gateway");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.IoTGatewayId).HasColumnName("IoT_Gateway_Id");

        entity.Property(e => e.Active)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.Description).HasMaxLength(500);

        entity.Property(e => e.GatewayName)
            .HasMaxLength(255)
            .HasColumnName("Gateway_Name");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.IoTGateways)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_IoT_Gateway_Tenant");
      });

      modelBuilder.Entity<MasterStreamType>(entity => {
        entity.HasKey(e => e.StreamTypeId);

        entity.ToTable("Master_Stream_Type");

        entity.Property(e => e.StreamTypeId)
            .ValueGeneratedNever()
            .HasColumnName("Stream_Type_Id");

        entity.Property(e => e.DataTypeId).HasColumnName("Data_Type_Id");

        entity.Property(e => e.StreamTypeName)
            .HasMaxLength(255)
            .HasColumnName("Stream_Type_Name");

        entity.HasOne(d => d.DataType)
            .WithMany(p => p.MasterStreamTypes)
            .HasForeignKey(d => d.DataTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Master_Stream_Type_Data_Type");
      });

      modelBuilder.Entity<MeasurementUnit>(entity => {
        entity.HasKey(e => e.UnitId);

        entity.ToTable("Measurement_Unit");

        entity.Property(e => e.UnitId)
            .ValueGeneratedNever()
            .HasColumnName("Unit_Id");

        entity.Property(e => e.MeasurementSystem)
            .HasMaxLength(10)
            .HasColumnName("Measurement_System");

        entity.Property(e => e.UnitAbbreviation)
            .HasMaxLength(25)
            .HasColumnName("Unit_Abbreviation");

        entity.Property(e => e.UnitGroup)
            .HasMaxLength(75)
            .HasColumnName("Unit_Group");
      });

      modelBuilder.Entity<OpcTag>(entity => {
        entity.HasKey(e => new { e.TenantId, e.TagId });

        entity.ToTable("OPC_Tag");

        entity.HasIndex(e => new { e.PhysicalAssetId, e.DataTypeId, e.StreamTypeId }, "IX_Physical_Asset_Id_Data_Type_Id_Stream_Type_Id");

        entity.HasIndex(e => new { e.TenantId, e.IoTGatewayId }, "IX_Tenant_Id_IoT_Gateway_Id")
            .HasFillFactor(90);

        entity.HasIndex(e => new { e.TenantId, e.IoTGatewayId, e.TagName }, "IX_Tenant_Id_IoT_Gateway_Id_Tag_Name")
            .IsUnique()
            .HasFillFactor(90);

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.TagId).HasColumnName("Tag_Id");

        entity.Property(e => e.DataTypeId).HasColumnName("Data_Type_Id");

        entity.Property(e => e.EventTypeId).HasColumnName("Event_Type_Id");

        entity.Property(e => e.IoTGatewayId).HasColumnName("IoT_Gateway_Id");

        entity.Property(e => e.LastGoodTimestamp).HasColumnName("Last_Good_Timestamp");

        entity.Property(e => e.LastGoodValue)
            .HasMaxLength(1000)
            .HasColumnName("Last_Good_Value");

        entity.Property(e => e.PhysicalAssetId).HasColumnName("Physical_Asset_Id");

        entity.Property(e => e.Quality).HasMaxLength(8);

        entity.Property(e => e.SeverityId).HasColumnName("Severity_Id");

        entity.Property(e => e.StreamTypeId).HasColumnName("Stream_Type_Id");

        entity.Property(e => e.TagAlias)
            .HasMaxLength(255)
            .HasColumnName("Tag_Alias");

        entity.Property(e => e.TagName)
            .HasMaxLength(255)
            .HasColumnName("Tag_Name");

        entity.Property(e => e.Value).HasMaxLength(1000);

        entity.Property(e => e.ValuePayload).HasColumnName("Value_Payload");

        entity.Property(e => e.SourceId).HasColumnName("Source_Id");

        entity.HasOne(d => d.DataType)
            .WithMany(p => p.OpcTags)
            .HasForeignKey(d => d.DataTypeId)
            .HasConstraintName("FK_OPC_Tag_Data_Type");

        entity.HasOne(d => d.EventType)
            .WithMany(p => p.OpcTags)
            .HasForeignKey(d => d.EventTypeId)
            .HasConstraintName("FK_OPC_Tag_Event_Type");

        entity.HasOne(d => d.IoTGateway)
            .WithMany(p => p.OpcTags)
            .HasForeignKey(d => new { d.TenantId, d.IoTGatewayId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OPC_Tag_IoT_Gateway");

        entity.HasOne(d => d.PhysicalAsset)
            .WithMany(p => p.OpcTags)
            .HasForeignKey(d => new { d.TenantId, d.PhysicalAssetId })
            .HasConstraintName("FK_OPC_Tag_Physical_Asset");

        entity.HasOne(d => d.StreamType)
            .WithMany(p => p.OpcTags)
            .HasForeignKey(d => new { d.TenantId, d.StreamTypeId })
            .HasConstraintName("FK_OPC_Tag_Stream_Type");
      });

      modelBuilder.Entity<OpcTagEvent>(entity => {
        entity.HasKey(e => new { e.TenantId, e.TagId });

        entity.ToTable("OPC_Tag_Event");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.TagId).HasColumnName("Tag_Id");

        entity.Property(e => e.EventTypeId).HasColumnName("Event_Type_Id");

        entity.Property(e => e.FalseAnnotation).HasColumnName("False_Annotation");

        entity.Property(e => e.TrueAnnotation).HasColumnName("True_Annotation");

        entity.HasOne(d => d.EventType)
            .WithMany(p => p.OpcTagEvents)
            .HasForeignKey(d => d.EventTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OPC_Tag_Event_Event_Type");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.OpcTagEvents)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OPC_Tag_Event_Tenant");

        entity.HasOne(d => d.EventReason)
            .WithMany(p => p.OpcTagEventEventReasons)
            .HasForeignKey(d => new { d.TenantId, d.FalseAnnotation })
            .HasConstraintName("FK_Opc_Tag_Event_Event_Reason_False_Annotation");

        entity.HasOne(d => d.T)
            .WithOne(p => p.OpcTagEvent)
            .HasForeignKey<OpcTagEvent>(d => new { d.TenantId, d.TagId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OPC_Tag_Event_OPC_Tag");

        entity.HasOne(d => d.TNavigation)
            .WithMany(p => p.OpcTagEventTNavigations)
            .HasForeignKey(d => new { d.TenantId, d.TrueAnnotation })
            .HasConstraintName("FK_Opc_Tag_Event_Event_Reason_True_Annotation");
      });

      modelBuilder.Entity<OpcTagStream>(entity => {
        entity.HasKey(e => new { e.TenantId, e.TagId });

        entity.ToTable("OPC_Tag_Stream");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.TagId).HasColumnName("Tag_Id");

        entity.Property(e => e.StreamTypeId).HasColumnName("Stream_Type_Id");

        entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.OpcTagStreams)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OPC_Tag_Stream_Tenant");

        entity.HasOne(d => d.Unit)
            .WithMany(p => p.OpcTagStreams)
            .HasForeignKey(d => d.UnitId)
            .HasConstraintName("FK_OPC_Tag_Stream_Measurement_Unit");

        entity.HasOne(d => d.StreamType)
            .WithMany(p => p.OpcTagStreams)
            .HasForeignKey(d => new { d.TenantId, d.StreamTypeId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OPC_Tag_Stream_Stream_Type");

        entity.HasOne(d => d.T)
            .WithOne(p => p.OpcTagStream)
            .HasForeignKey<OpcTagStream>(d => new { d.TenantId, d.TagId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OPC_Tag_Stream_OPC_Tag");
      });

      modelBuilder.Entity<OpcTagThreshold>(entity => {
        entity.HasKey(e => new { e.TenantId, e.ThresholdTagId });

        entity.ToTable("OPC_Tag_Threshold");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.ThresholdTagId).HasColumnName("Threshold_Tag_Id");

        entity.Property(e => e.OpcTagId).HasColumnName("OPC_Tag_Id");

        entity.Property(e => e.Timestamp).HasDefaultValueSql("(getutcdate())");

        entity.HasOne(d => d.OpcTag)
            .WithMany(p => p.OpcTagThresholdOpcTags)
            .HasForeignKey(d => new { d.TenantId, d.OpcTagId })
            .HasConstraintName("FK_OPC_Tag_Threshold_OPC_Tag");

        entity.HasOne(d => d.T)
            .WithOne(p => p.OpcTagThresholdT)
            .HasForeignKey<OpcTagThreshold>(d => new { d.TenantId, d.ThresholdTagId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OPC_Tag_Threshold_OPC_Threshold");
      });

      modelBuilder.Entity<Permission>(entity => {
        entity.ToTable("Permission");

        entity.Property(e => e.PermissionId)
            .ValueGeneratedNever()
            .HasColumnName("Permission_Id");

        entity.Property(e => e.Active)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.PermissionCode)
            .HasMaxLength(100)
            .HasColumnName("Permission_Code");

        entity.Property(e => e.PermissionDisplayName)
            .HasMaxLength(255)
            .HasColumnName("Permission_Display_Name");
      });

      modelBuilder.Entity<PhysicalAsset>(entity => {
        entity.HasKey(e => new { e.TenantId, e.PhysicalAssetId });

        entity.ToTable("Physical_Asset");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.PhysicalAssetId).HasColumnName("Physical_Asset_Id");

        entity.Property(e => e.Active)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.AssetCode)
            .HasMaxLength(255)
            .HasColumnName("Asset_Code")
            .HasDefaultValueSql("(N'')");

        entity.Property(e => e.AssetSerialNumber)
            .HasMaxLength(255)
            .HasColumnName("Asset_Serial_Number");

        entity.Property(e => e.Description).HasMaxLength(500);

        entity.Property(e => e.PhysicalAssetName)
            .HasMaxLength(255)
            .HasColumnName("Physical_Asset_Name");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.PhysicalAssets)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_Tenant");
      });

      modelBuilder.Entity<PhysicalAssetAttribute>(entity => {
        entity.HasKey(e => new { e.TenantId, e.PhysicalAssetId, e.AttributeValueId });

        entity.ToTable("Physical_Asset_Attribute");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.PhysicalAssetId).HasColumnName("Physical_Asset_Id");

        entity.Property(e => e.AttributeValueId).HasColumnName("Attribute_Value_Id");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.PhysicalAssetAttributes)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_Attribute_Tenant");

        entity.HasOne(d => d.AttributeValue)
            .WithMany(p => p.PhysicalAssetAttributes)
            .HasForeignKey(d => new { d.TenantId, d.AttributeValueId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_Attribute_Attribute_Value");

        entity.HasOne(d => d.PhysicalAsset)
            .WithMany(p => p.PhysicalAssetAttributes)
            .HasForeignKey(d => new { d.TenantId, d.PhysicalAssetId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_Attribute_Physical_Asset");
      });

      modelBuilder.Entity<PhysicalAssetBridgeAttribute>(entity => {
        entity.HasKey(e => new { e.TenantId, e.PhysicalAssetId, e.BridgeAttributeId, e.PhysicalAssetBridgeAttributeId });

        entity.ToTable("Physical_Asset_Bridge_Attribute");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.PhysicalAssetId).HasColumnName("Physical_Asset_Id");

        entity.Property(e => e.BridgeAttributeId).HasColumnName("Bridge_Attribute_Id");

        entity.Property(e => e.PhysicalAssetBridgeAttributeId).HasColumnName("Physical_Asset_Bridge_Attribute_Id");

        entity.Property(e => e.ValuePayload).HasColumnName("Value_Payload");

        entity.HasOne(d => d.BridgeAttribute)
            .WithMany(p => p.PhysicalAssetBridgeAttributes)
            .HasForeignKey(d => d.BridgeAttributeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_Bridge_Attribute_Bridge_Attribute");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.PhysicalAssetBridgeAttributes)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_Bridge_Attribute_Tenant");

        entity.HasOne(d => d.PhysicalAsset)
            .WithMany(p => p.PhysicalAssetBridgeAttributes)
            .HasForeignKey(d => new { d.TenantId, d.PhysicalAssetId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_Bridge_Attribute_Physical_Asset");
      });

      modelBuilder.Entity<PhysicalAssetIoTGateway>(entity => {
        entity.HasKey(e => new { e.TenantId, e.PhysicalAssetId, e.IoTGatewayId });

        entity.ToTable("Physical_Asset_IoT_Gateway");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.PhysicalAssetId).HasColumnName("Physical_Asset_Id");

        entity.Property(e => e.IoTGatewayId).HasColumnName("IoT_Gateway_Id");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.PhysicalAssetIoTGateways)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_IoT_Gateway_Tenant");

        entity.HasOne(d => d.IoTGateway)
            .WithMany(p => p.PhysicalAssetIoTGateways)
            .HasForeignKey(d => new { d.TenantId, d.IoTGatewayId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_IoT_Gateway_IoT_Gateway");

        entity.HasOne(d => d.PhysicalAsset)
            .WithMany(p => p.PhysicalAssetIoTGateways)
            .HasForeignKey(d => new { d.TenantId, d.PhysicalAssetId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_IoT_Gateway_Physical_Asset");
      });

      modelBuilder.Entity<PhysicalAssetIoTGatewayReportedStatusR1>(entity => {
        entity.HasKey(e => new { e.TenantId, e.PhysicalAssetId, e.IoTGatewayId });

        entity.ToTable("Physical_Asset_IoT_Gateway_Reported_Status_R1");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.PhysicalAssetId).HasColumnName("Physical_Asset_Id");

        entity.Property(e => e.IoTGatewayId).HasColumnName("IoT_Gateway_Id");

        entity.Property(e => e.IdleStatus).HasColumnName("Idle_Status");

        entity.Property(e => e.InCycleStatus).HasColumnName("In_Cycle_Status");

        entity.Property(e => e.OffStatus).HasColumnName("Off_Status");

        entity.Property(e => e.ProblemStatus).HasColumnName("Problem_Status");

        entity.Property(e => e.ReportedDate).HasColumnName("Reported_Date");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.PhysicalAssetIoTGatewayReportedStatusR1s)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_IoT_Gateway_Reported_Status_R1_Tenant");

        entity.HasOne(d => d.IoTGateway)
            .WithMany(p => p.PhysicalAssetIoTGatewayReportedStatusR1s)
            .HasForeignKey(d => new { d.TenantId, d.IoTGatewayId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_IoT_Gateway_Reported_Status_R1_IoT_Gateway");

        entity.HasOne(d => d.PhysicalAsset)
            .WithMany(p => p.PhysicalAssetIoTGatewayReportedStatusR1s)
            .HasForeignKey(d => new { d.TenantId, d.PhysicalAssetId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_IoT_Gateway_Reported_Status_R1_Physical_Asset");
      });

      modelBuilder.Entity<PhysicalAssetOpcStatusConfigR1>(entity => {
        entity.HasKey(e => new { e.TenantId, e.PhysicalAssetId });

        entity.ToTable("Physical_Asset_OPC_Status_Config_R1");

        entity.HasIndex(e => new { e.TenantId, e.IoTGatewayId, e.PhysicalAssetId }, "IX_Tenant_Id_IoT_Gateway_Id_Physical_Asset_Id")
            .HasFillFactor(90);

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.PhysicalAssetId).HasColumnName("Physical_Asset_Id");

        entity.Property(e => e.IdleStatusTag)
            .HasMaxLength(255)
            .HasColumnName("Idle_Status_Tag");

        entity.Property(e => e.InCycleStatusTag)
            .HasMaxLength(255)
            .HasColumnName("In_Cycle_Status_Tag");

        entity.Property(e => e.IoTGatewayId).HasColumnName("IoT_Gateway_Id");

        entity.Property(e => e.OffStatusTag)
            .HasMaxLength(255)
            .HasColumnName("Off_Status_Tag");

        entity.Property(e => e.ProblemStatusTag)
            .HasMaxLength(255)
            .HasColumnName("Problem_Status_Tag");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.PhysicalAssetOpcStatusConfigR1s)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_OPC_Status_Config_R1_Tenant");

        entity.HasOne(d => d.IoTGateway)
            .WithMany(p => p.PhysicalAssetOpcStatusConfigR1s)
            .HasForeignKey(d => new { d.TenantId, d.IoTGatewayId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_OPC_Status_Config_R1_IoT_Gateway");

        entity.HasOne(d => d.PhysicalAsset)
            .WithOne(p => p.PhysicalAssetOpcStatusConfigR1)
            .HasForeignKey<PhysicalAssetOpcStatusConfigR1>(d => new { d.TenantId, d.PhysicalAssetId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_OPC_Status_Config_R1_Physical_Asset");
      });

      modelBuilder.Entity<PhysicalAssetOpcStatusPayloadR1>(entity => {
        entity.HasKey(e => new { e.TenantId, e.PhysicalAssetId });

        entity.ToTable("Physical_Asset_OPC_Status_Payload_R1");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.PhysicalAssetId).HasColumnName("Physical_Asset_Id");

        entity.Property(e => e.IoTGatewayId).HasColumnName("IoT_Gateway_Id");

        entity.Property(e => e.UpdateDate).HasColumnName("Update_Date");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.PhysicalAssetOpcStatusPayloadR1s)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_OPC_Status_Payload_R1_Tenant");

        entity.HasOne(d => d.IoTGateway)
            .WithMany(p => p.PhysicalAssetOpcStatusPayloadR1s)
            .HasForeignKey(d => new { d.TenantId, d.IoTGatewayId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_OPC_Status_Payload_R1_IoT_Gateway");

        entity.HasOne(d => d.PhysicalAsset)
            .WithOne(p => p.PhysicalAssetOpcStatusPayloadR1)
            .HasForeignKey<PhysicalAssetOpcStatusPayloadR1>(d => new { d.TenantId, d.PhysicalAssetId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Physical_Asset_OPC_Status_Payload_R1_Physical_Asset");
      });

      modelBuilder.Entity<ResourceAuditLog>(entity => {
        entity.HasKey(e => new { e.TenantId, e.ResourceAuditLogKey });

        entity.ToTable("Resource_Audit_Log");

        entity.HasIndex(e => new { e.TenantId, e.TableName }, "IX_Tenant_Id_Table_Name");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.ResourceAuditLogKey)
            .ValueGeneratedOnAdd()
            .HasColumnName("Resource_Audit_Log_Key");

        entity.Property(e => e.AccountId).HasColumnName("Account_Id");

        entity.Property(e => e.ActionDate).HasColumnName("Action_Date");

        entity.Property(e => e.ActionType)
            .HasMaxLength(255)
            .HasColumnName("Action_Type");

        entity.Property(e => e.ResourceId).HasColumnName("Resource_Id");

        entity.Property(e => e.TableName)
            .HasMaxLength(255)
            .HasColumnName("Table_Name");
      });

      modelBuilder.Entity<Role>(entity => {
        entity.ToTable("Role");

        entity.Property(e => e.RoleId)
            .ValueGeneratedNever()
            .HasColumnName("Role_Id");

        entity.Property(e => e.Active)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.RoleDisplayName)
            .HasMaxLength(255)
            .HasColumnName("Role_Display_Name");

        entity.HasMany(d => d.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity<Dictionary<string, object>>(
                "RolePermission",
                l => l.HasOne<Permission>().WithMany().HasForeignKey("PermissionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Role_Permision_Permission"),
                r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Role_Permision_Role"),
                j => {
                  j.HasKey("RoleId", "PermissionId");

                  j.ToTable("Role_Permission");

                  j.IndexerProperty<Guid>("RoleId").HasColumnName("Role_Id");

                  j.IndexerProperty<Guid>("PermissionId").HasColumnName("Permission_Id");
                });
      });

      modelBuilder.Entity<SchemaVersion>(entity => {
        entity.Property(e => e.Applied).HasColumnType("datetime");

        entity.Property(e => e.ScriptName).HasMaxLength(255);
      });

      modelBuilder.Entity<StreamType>(entity => {
        entity.HasKey(e => new { e.TenantId, e.StreamTypeId })
            .HasName("PK_String_Stream_Type");

        entity.ToTable("Stream_Type");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.StreamTypeId).HasColumnName("Stream_Type_Id");

        entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

        entity.Property(e => e.CreatedTimestamp).HasColumnName("Created_Timestamp");

        entity.Property(e => e.DataTypeId).HasColumnName("Data_Type_Id");

        entity.Property(e => e.IsDefaultStream).HasColumnName("Is_Default_Stream");

        entity.Property(e => e.StreamTypeName)
            .HasMaxLength(255)
            .HasColumnName("Stream_Type_Name");

        entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

        entity.Property(e => e.UpdatedTimestamp).HasColumnName("Updated_Timestamp");

        entity.HasOne(d => d.DataType)
            .WithMany(p => p.StreamTypes)
            .HasForeignKey(d => d.DataTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Stream_Type_Data_Type");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.StreamTypes)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_String_Stream_Type_Tenant");
      });

      modelBuilder.Entity<SystemInformationBuildDatum>(entity => {
        entity.HasKey(e => e.SystemInformationBuildDataId);

        entity.ToTable("System_Information_Build_Data");

        entity.Property(e => e.SystemInformationBuildDataId)
            .HasColumnName("System_Information_Build_Data_Id")
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.AppliedDate).HasColumnName("Applied_Date");

        entity.Property(e => e.BuildNumber)
            .HasMaxLength(50)
            .HasColumnName("Build_Number")
            .HasDefaultValueSql("('')");
      });

      modelBuilder.Entity<TagEventDatum>(entity => {
        entity.HasKey(e => new { e.TenantId, e.TagEventId })
            .HasName("PK_Event_Data");

        entity.ToTable("Tag_Event_Data");

        entity.HasIndex(e => new { e.TenantId, e.OpcGatewayId, e.TagName, e.EventTypeId }, "IX_Tenant_Id_IoT_Gateway_Id_Tag_Name_Event_Type_Id")
            .IsUnique();

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.TagEventId).HasColumnName("Tag_Event_Id");

        entity.Property(e => e.DataTypeId).HasColumnName("Data_Type_Id");

        entity.Property(e => e.EventId).HasColumnName("Event_Id");

        entity.Property(e => e.EventReason)
            .HasMaxLength(255)
            .HasColumnName("Event_Reason");

        entity.Property(e => e.EventTypeId).HasColumnName("Event_Type_Id");

        entity.Property(e => e.IoTGatewayId).HasColumnName("IoT_Gateway_Id");

        entity.Property(e => e.OpcGatewayId).HasColumnName("OPC_Gateway_Id");

        entity.Property(e => e.PhysicalAssetId).HasColumnName("Physical_Asset_Id");

        entity.Property(e => e.TagName)
            .HasMaxLength(255)
            .HasColumnName("Tag_Name");

        entity.HasOne(d => d.DataType)
            .WithMany(p => p.TagEventData)
            .HasForeignKey(d => d.DataTypeId)
            .HasConstraintName("FK_Event_Data_Data_Type");

        entity.HasOne(d => d.PhysicalAsset)
            .WithMany(p => p.TagEventData)
            .HasForeignKey(d => new { d.TenantId, d.PhysicalAssetId })
            .HasConstraintName("FK_Event_Data_Physical_Asset");
      });

      modelBuilder.Entity<Tenant>(entity => {
        entity.ToTable("Tenant");

        entity.HasIndex(e => new { e.EnterpriseId, e.TenantId }, "IX_Enterprise_Id_Tenant_Id");

        entity.Property(e => e.TenantId)
            .ValueGeneratedNever()
            .HasColumnName("Tenant_Id");

        entity.Property(e => e.Active)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.EnterpriseId).HasColumnName("Enterprise_Id");

        entity.Property(e => e.PmcEnabled)
            .IsRequired()
            .HasColumnName("PMC_Enabled")
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.TenantCode)
            .HasMaxLength(255)
            .HasColumnName("Tenant_Code");

        entity.Property(e => e.TenantName)
            .HasMaxLength(255)
            .HasColumnName("Tenant_Name");

        entity.HasOne(d => d.Enterprise)
            .WithMany(p => p.Tenants)
            .HasForeignKey(d => d.EnterpriseId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Site_Enterprise");
      });

      modelBuilder.Entity<UserAccount>(entity => {
        entity.HasKey(e => new { e.TenantId, e.AccountId });

        entity.ToTable("User_Account");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.AccountId).HasColumnName("Account_Id");

        entity.Property(e => e.Active)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.AdminUser).HasColumnName("Admin_User");

        entity.Property(e => e.DisplayName)
            .HasMaxLength(255)
            .HasColumnName("Display_Name");

        entity.Property(e => e.Email)
            .HasMaxLength(500)
            .HasDefaultValueSql("(N'')");

        entity.HasOne(d => d.Tenant)
            .WithMany(p => p.UserAccounts)
            .HasForeignKey(d => d.TenantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_User_Account_Tenant");

        entity.HasMany(d => d.Roles)
            .WithMany(p => p.UserAccounts)
            .UsingEntity<Dictionary<string, object>>(
                "UserAccountRole",
                l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_User_Account_Role_Role"),
                r => r.HasOne<UserAccount>().WithMany().HasForeignKey("TenantId", "AccountId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_User_Account_Role_User_Account"),
                j => {
                  j.HasKey("TenantId", "AccountId", "RoleId");

                  j.ToTable("User_Account_Role");

                  j.IndexerProperty<Guid>("TenantId").HasColumnName("Tenant_Id");

                  j.IndexerProperty<Guid>("AccountId").HasColumnName("Account_Id");

                  j.IndexerProperty<Guid>("RoleId").HasColumnName("Role_Id");
                });
      });

      modelBuilder.Entity<UserAccountSetting>(entity => {
        entity.HasKey(e => new { e.TenantId, e.AccountId });

        entity.ToTable("User_Account_Setting");

        entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

        entity.Property(e => e.AccountId).HasColumnName("Account_Id");

        entity.Property(e => e.DateFormat)
            .HasMaxLength(100)
            .HasColumnName("Date_Format");

        entity.Property(e => e.DisplayTheme)
            .HasMaxLength(100)
            .HasColumnName("Display_Theme");

        entity.Property(e => e.NumberFormat)
            .HasMaxLength(100)
            .HasColumnName("Number_Format");

        entity.Property(e => e.TimeFormat)
            .HasMaxLength(100)
            .HasColumnName("Time_Format");

        entity.Property(e => e.Timezone).HasMaxLength(100);
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
