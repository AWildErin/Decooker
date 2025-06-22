using Decooker.Objects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Deserialization;

/// <summary>
/// Interface that goes together with UnrealStructAttribute.
/// </summary>
public interface IUnrealAtomicStruct
{
	public void ReadStruct( UnrealReader Reader, UStructProperty Struct );
}
