  
  
  
  
  
  
using System.Collections.Generic;

namespace MyNameSpace{  

  
    public partial class Catalog  
    {  
  
      public IEnumerable<Artist>Artists {get;set;}
  
      public IEnumerable<Book>Books {get;set;}
  
   }
  
    public partial class Artist  
    {  
  
      public IEnumerable<Song>Songs {get;set;}
  
      public string Id     {get;set;}
  
      public string Name     {get;set;}
  
   }
  
    public partial class Song  
    {  
  
      public string Id     {get;set;}
  
   }
  
    public partial class Book  
    {  
  
      public string Id     {get;set;}
  
      public string Name     {get;set;}
  
      public string OneProperty     {get;set;}
  
   }
  
}
