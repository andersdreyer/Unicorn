﻿using Unicorn.Data;

namespace Unicorn.Serialization
{
	public interface ISerializationProvider
	{
		/// <summary>
		/// This moniker will be used when logging actions taken by this provider
		/// </summary>
		string LogName { get; }

		/// <summary>
		/// Serialize a given item into the serialization provider's serialization store
		/// </summary>
		ISerializedItem SerializeItem(ISourceItem item);

		/// <summary>
		/// Writes an updated serialized item to the serialization provider's store
		/// </summary>
		void UpdateSerializedItem(ISerializedItem serializedItem);
		
		/// <summary>
		/// Rename a serialized item in the provider. Note that the source item may NOT exist already in the provider.
		/// </summary>
		void RenameSerializedItem(ISourceItem renamedItem, string oldName);

		/// <summary>
		/// Move a serialized item in the provider. Note that the sourceItem may NOT exist already in the provider.
		/// </summary>
		/// <param name="sourceItem"></param>
		/// <param name="newParentItem"></param>
		void MoveSerializedItem(ISourceItem sourceItem, ISourceItem newParentItem);
		
		/// <summary>
		/// Deletes an item from the serialization provider.
		/// </summary>
		/// <param name="item">The item to delete. If it's an ISerializedReference, it must refer to an item path and not a directory path.</param>
		void DeleteSerializedItem(ISerializedReference item);

		/// <summary>
		/// Deserialize a given item from the provider's serialization store to Sitecore database
		/// </summary>
		/// <remarks>
		/// Should throw an exception if deserialization fails, probably a DeserializationException
		/// </remarks>
		ISourceItem DeserializeItem(ISerializedItem serializedItem, bool ignoreMissingTemplateFields);

		/// <summary>
		/// Get a serialized reference for a given Sitecore path and database. A reference is a pointer to the path,
		/// and while it indicates the store is capable of storing the path, it does not guarantee that path is serialized.
		/// </summary>
		ISerializedReference GetReference(ISourceItem sourceItem);

		/// <summary>
		/// Get a serialized item from a serialized reference. This dereferences a reference into an actual serialized item.
		/// Unlike a reference, this should return null if the reference path is not actually serialized.
		/// </summary>
		ISerializedItem GetItem(ISerializedReference reference);

		/// <summary>
		/// Get all child references for a given serialized reference parent
		/// </summary>
		ISerializedReference[] GetChildReferences(ISerializedReference parent, bool recursive);

		/// <summary>
		/// Get all child serialized items for a given serialized reference parent
		/// </summary>
		ISerializedItem[] GetChildItems(ISerializedReference parent);

		/// <summary>
		/// Checks if a serialized item is part of a template standard values
		/// </summary>
		/// <param name="item">Item to test</param>
		/// <returns>True if the item is a standard values item, or false if it's not</returns>
		bool IsStandardValuesItem(ISerializedItem item);
	}
}