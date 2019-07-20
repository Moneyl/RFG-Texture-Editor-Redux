using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace PEGEditor.Properties
{
	// Token: 0x0200000F RID: 15
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002E4E File Offset: 0x0000104E
		internal Resources()
		{
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002E58 File Offset: 0x00001058
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("PEGEditor.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002E97 File Offset: 0x00001097
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002E9E File Offset: 0x0000109E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002EA8 File Offset: 0x000010A8
		internal static Bitmap Checker
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Checker", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002ED0 File Offset: 0x000010D0
		internal static Bitmap Checker1
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Checker1", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002EF8 File Offset: 0x000010F8
		internal static Bitmap Checker2
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Checker2", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x04000037 RID: 55
		private static ResourceManager resourceMan;

		// Token: 0x04000038 RID: 56
		private static CultureInfo resourceCulture;
	}
}
