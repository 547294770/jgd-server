<template>  
    <h1 @click='say'>{{msg}}</h1>  
</template>  
<script>  
    export default {  
      data () {  
        return {  
          msg: 'Hello Vue'  
        }  
      },  
      methods : {  
        say : function(){  
            alert('Hello Vue');  
        }  
      }  
    }  
</script>  
<style>  
    h1{color:red;}  
</style>  