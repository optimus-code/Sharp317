using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp317
{
    /**
        * Filters out fast and normal client connection speeds.
        * SYI/DoS Flooding Protection.
        * @author Gnarly
        * 
        */
    public class ConnectionFilter
    {

        private static List<String> HOST_LIST = new List<String>();
        private static List<String> CONNECTIONS = new List<String>();

        private int cycle;
        private Boolean isFlooder;
        private Boolean wasFlooder;
        private Boolean wasConnected;

        public ConnectionFilter( )
        {
            Console.WriteLine( "[ConnectionFilter] - Initialized" );
        }

        /**
         * Setter for isFlooder Boolean
         * @param isFlooder
         */
        private void setFlooder( Boolean isFlooder )
        {
            this.isFlooder = isFlooder;
        }

        /**
         * Setter for wasFlooder Boolean
         * @param wasFlooder
         */
        private void setWasFlooder( Boolean wasFlooder )
        {
            this.wasFlooder = wasFlooder;
        }

        /**
         * Setter for wasConnected Boolean
         * @param wasConnected
         */
        private void setWasConnected( Boolean wasConnected )
        {
            this.wasConnected = wasConnected;
        }
        
        /**
         * @return isWasFlooder
         */
        private Boolean isWasFlooder( )
        {
            return wasFlooder;
        }

        /**
         * @return wasConnected
         */
        private Boolean isWasConnected( )
        {
            return wasConnected;
        }

        /**
         * Setter for HOST_LIST array
         * @param hOST_LIST List
         */
        public static void setHOST_LIST( List<String> hOST_LIST )
        {
            HOST_LIST = hOST_LIST;
        }

        /**
         * @return List HOST_LIST
         * @param getHOST_LIST List
         */
        public static List<String> getHOST_LIST( )
        {
            return HOST_LIST;
        }

        /**
         * Setter for CONNECTIONS array
         * @param cONNECTIONS List
         */
        public static void setCONNECTIONS( List<String> cONNECTIONS )
        {
            CONNECTIONS = cONNECTIONS;
        }

        /**
         * @return List CONNECTIONS
         * @param getConnections List
         */
        public static List<String> getCONNECTIONS( )
        {
            return CONNECTIONS;
        }

        /**
         * Adds a hostname to the HOST_LIST array list
         * @param host String
         */
        public void add( String host )
        {
            Console.WriteLine( ( new StringBuilder( "[ConnectionFilter] Temporarily banning " ) ).Append( host ).Append( " (Massive Connecting Flooding)" ).ToString() );
            getHOST_LIST().Add( host );
        }

        /**
         * Checks for any blocked hostnames
         * @param host String
         */
        public Boolean blocked( String host )
        {
            foreach ( String h in HOST_LIST )
            {
                if ( h.Equals( host ) )
                    return true;
            }

            int n = 0;
            foreach ( String h in CONNECTIONS )
            {
                if ( host.Equals( h ) )
                {
                    n++;
                }
            }

            if ( n > 2 )
            {
                add( host );
                setFlooder( true );
                return true;
            }

            if ( isFlooder )
                return true;

            if ( isWasFlooder() )
            {
                /* Handle anything you want to do to a person that tryed flooding the server in the past. */
            }
            return false;
        }

        /**
         * 500ms Process, used for clearing out the array's after a given time in the cycle
         */
        public void process( )
        {
            if ( cycle % 10 == 0 )
            {
                getCONNECTIONS().Clear();
                setWasConnected( true );
            }
            if ( cycle % 500 == 0 )
            {
                getHOST_LIST().Clear();
                setFlooder( false );
                setWasFlooder( true );
            }
            if ( cycle > 10000 )
            {
                cycle = 0;
            }
            cycle++;
        }

    }
}
