'If anyone can convert this to c# and get it to still work, please do :)

Public Module Compression

#Region " Char Index "

    Dim CharIndex() As UInt32 = New UInt32() { _
 &H247, &H236, &H225, &H214, &H203, &H1F2, &H1E1, &H1D0, _
 &H1BF, &H1AE, &H19D, &H18C, &H17B, &H16A, &H161, &H158, _
 &H14F, &H146, &H13D, &H134, &H12B, &H122, &H119, &H110, _
 &H107, &HFE, &HF5, &HEC, &HE3, &HDA, &HD1, &HC8, _
 &HBF, &HB6, &HAD, &HA8, &HA3, &H9E, &H99, &H94, _
 &H8F, &H8A, &H85, &H80, &H7B, &H76, &H71, &H6C, _
 &H69, &H66, &H63, &H60, &H5D, &H5A, &H57, &H54, _
 &H51, &H4E, &H4B, &H48, &H45, &H42, &H3F, &H3F, _
 &H3C, &H3C, &H39, &H39, &H36, &H36, &H33, &H33, _
 &H30, &H30, &H2D, &H2D, &H2A, &H2A, &H27, &H27, _
 &H24, &H24, &H21, &H21, &H1E, &H1E, &H1B, &H1B, _
 &H18, &H18, &H15, &H15, &H12, &H12, &H12, &H12, _
 &HF, &HF, &HF, &HF, &HC, &HC, &HC, &HC, _
 &H9, &H9, &H9, &H9, &H6, &H6, &H6, &H6, _
 &H3, &H3, &H3, &H3, &H3, &H3, &H3, &H3, _
 &H3, &H3, &H3, &H3, &H3, &H3, &H3, &H3, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, _
 &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}

#End Region

#Region " Char Table "

    Dim CharTable() As Byte = New Byte() { _
 &H0, &H0, &H1, &H0, &H1, &H4, &H0, &HFF, &H6, &H0, &H14, &H6, _
 &H0, &H13, &H6, &H0, &H5, &H6, &H0, &H2, &H6, &H0, &H80, &H7, _
 &H0, &H6D, &H7, &H0, &H69, &H7, &H0, &H68, &H7, &H0, &H67, &H7, _
 &H0, &H1E, &H7, &H0, &H15, &H7, &H0, &H12, &H7, &H0, &HD, &H7, _
 &H0, &HA, &H7, &H0, &H8, &H7, &H0, &H7, &H7, &H0, &H6, &H7, _
 &H0, &H4, &H7, &H0, &H3, &H7, &H0, &H6C, &H8, &H0, &H51, &H8, _
 &H0, &H20, &H8, &H0, &H1F, &H8, &H0, &H1D, &H8, &H0, &H18, &H8, _
 &H0, &H17, &H8, &H0, &H16, &H8, &H0, &H11, &H8, &H0, &H10, &H8, _
 &H0, &HF, &H8, &H0, &HC, &H8, &H0, &HB, &H8, &H0, &H9, &H8, _
 &H1, &H96, &H9, &H97, &H9, &H1, &H90, &H9, &H95, &H9, &H1, &H64, _
 &H9, &H6B, &H9, &H1, &H62, &H9, &H63, &H9, &H1, &H56, &H9, &H58, _
 &H9, &H1, &H52, &H9, &H55, &H9, &H1, &H4D, &H9, &H50, &H9, &H1, _
 &H45, &H9, &H4C, &H9, &H1, &H40, &H9, &H43, &H9, &H1, &H31, &H9, _
 &H3B, &H9, &H1, &H28, &H9, &H30, &H9, &H1, &H1A, &H9, &H25, &H9, _
 &H1, &HE, &H9, &H19, &H9, &H2, &HE2, &HA, &HE8, &HA, &HF0, &HA, _
 &HF8, &HA, &H2, &HC0, &HA, &HC2, &HA, &HCE, &HA, &HE0, &HA, &H2, _
 &HA0, &HA, &HA2, &HA, &HB0, &HA, &HB8, &HA, &H2, &H8A, &HA, &H8F, _
 &HA, &H93, &HA, &H98, &HA, &H2, &H81, &HA, &H82, &HA, &H83, &HA, _
 &H89, &HA, &H2, &H7C, &HA, &H7D, &HA, &H7E, &HA, &H7F, &HA, &H2, _
 &H77, &HA, &H78, &HA, &H79, &HA, &H7A, &HA, &H2, &H73, &HA, &H74, _
 &HA, &H75, &HA, &H76, &HA, &H2, &H6E, &HA, &H6F, &HA, &H70, &HA, _
 &H72, &HA, &H2, &H61, &HA, &H65, &HA, &H66, &HA, &H6A, &HA, &H2, _
 &H5D, &HA, &H5E, &HA, &H5F, &HA, &H60, &HA, &H2, &H57, &HA, &H59, _
 &HA, &H5A, &HA, &H5B, &HA, &H2, &H4A, &HA, &H4B, &HA, &H4E, &HA, _
 &H53, &HA, &H2, &H46, &HA, &H47, &HA, &H48, &HA, &H49, &HA, &H2, _
 &H3F, &HA, &H41, &HA, &H42, &HA, &H44, &HA, &H2, &H3A, &HA, &H3C, _
 &HA, &H3D, &HA, &H3E, &HA, &H2, &H36, &HA, &H37, &HA, &H38, &HA, _
 &H39, &HA, &H2, &H32, &HA, &H33, &HA, &H34, &HA, &H35, &HA, &H2, _
 &H2B, &HA, &H2C, &HA, &H2D, &HA, &H2E, &HA, &H2, &H26, &HA, &H27, _
 &HA, &H29, &HA, &H2A, &HA, &H2, &H21, &HA, &H22, &HA, &H23, &HA, _
 &H24, &HA, &H3, &HFB, &HB, &HFC, &HB, &HFD, &HB, &HFE, &HB, &H1B, _
 &HA, &H1B, &HA, &H1C, &HA, &H1C, &HA, &H3, &HF2, &HB, &HF3, &HB, _
 &HF4, &HB, &HF5, &HB, &HF6, &HB, &HF7, &HB, &HF9, &HB, &HFA, &HB, _
 &H3, &HE9, &HB, &HEA, &HB, &HEB, &HB, &HEC, &HB, &HED, &HB, &HEE, _
 &HB, &HEF, &HB, &HF1, &HB, &H3, &HDE, &HB, &HDF, &HB, &HE1, &HB, _
 &HE3, &HB, &HE4, &HB, &HE5, &HB, &HE6, &HB, &HE7, &HB, &H3, &HD6, _
 &HB, &HD7, &HB, &HD8, &HB, &HD9, &HB, &HDA, &HB, &HDB, &HB, &HDC, _
 &HB, &HDD, &HB, &H3, &HCD, &HB, &HCF, &HB, &HD0, &HB, &HD1, &HB, _
 &HD2, &HB, &HD3, &HB, &HD4, &HB, &HD5, &HB, &H3, &HC5, &HB, &HC6, _
 &HB, &HC7, &HB, &HC8, &HB, &HC9, &HB, &HCA, &HB, &HCB, &HB, &HCC, _
 &HB, &H3, &HBB, &HB, &HBC, &HB, &HBD, &HB, &HBE, &HB, &HBF, &HB, _
 &HC1, &HB, &HC3, &HB, &HC4, &HB, &H3, &HB2, &HB, &HB3, &HB, &HB4, _
 &HB, &HB5, &HB, &HB6, &HB, &HB7, &HB, &HB9, &HB, &HBA, &HB, &H3, _
 &HA9, &HB, &HAA, &HB, &HAB, &HB, &HAC, &HB, &HAD, &HB, &HAE, &HB, _
 &HAF, &HB, &HB1, &HB, &H3, &H9F, &HB, &HA1, &HB, &HA3, &HB, &HA4, _
 &HB, &HA5, &HB, &HA6, &HB, &HA7, &HB, &HA8, &HB, &H3, &H92, &HB, _
 &H94, &HB, &H99, &HB, &H9A, &HB, &H9B, &HB, &H9C, &HB, &H9D, &HB, _
 &H9E, &HB, &H3, &H86, &HB, &H87, &HB, &H88, &HB, &H8B, &HB, &H8C, _
 &HB, &H8D, &HB, &H8E, &HB, &H91, &HB, &H3, &H2F, &HB, &H4F, &HB, _
 &H54, &HB, &H5C, &HB, &H71, &HB, &H7B, &HB, &H84, &HB, &H85, &HB}

#End Region

#Region " Bit Masks "

    Dim BitMasks() As UInt32 = New UInt32() { _
     &H0, &H1, &H3, &H7, &HF, &H1F, &H3F, &H7F, _
     &HFF, &H1FF, &H3FF, &H7FF, &HFFF, &H1FFF, &H3FFF, &H7FFF}

#End Region

#Region " Packet Sizes "

    Dim PacketSizes() As Integer = New Integer() {1, 8, 1, 12, 1, 1, 1, 6, 6, 11, 6, 6, 9, 13, 12, &H10, &H10, 8, &H1A, 14, &H12, 11, -1, -1, 15, 2, 2, 3, 5, 3, 4, 6, 10, 12, 12, 13, 90, 90, -1, 40, &H67, &H61, 15, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, &H22, 8, 13, 0, 6, 0, 0, 13, 0, 11, 11, 0, 0, 0, &H10, &H11, 7, 1, 15, 14, &H2A, 10, 3, 0, 0, 14, 7, &H1A, 40, -1, 5, 6, &H26, 5, 7, 2, 7, &H15, 0, 7, 7, &H10, &H15, 12, 12, &H10, &H10, 10, 1, 1, 1, 1, 1, &H20, 10, 13, 6, 2, &H15, 6, 13, 8, 6, &H12, 5, 10, 4, 20, &H1D, 0, 0, 0, 0, 0, 0, 2, 6, 6, 11, 7, 10, &H21, 13, &H1A, 6, 8, -1, 13, 9, 1, 7, &H10, &H11, 7, -1, -1, 7, 8, 10, 7, 8, &H18, 3, 8, -1, 7, -1, 7, -1, 7, -1, 0, -1, 0, 1}

#End Region

#Region " Compression Table "

    '&H80010000 is not a valid uint32 so i removed the last 0 so it now shows as &H800100, this will probably cause problems
    Dim CompressionTable() As UInt32 = New UInt32() { _
     2147549184, &H70040000, &H5C060000, &H3E070000, &H40070000, &H60060000, &H42070000, &H44070000, _
     &H46070000, &H30080000, &H48070000, &H31080000, &H32080000, &H4A070000, &H23080100, &H33080000, _
     &H34080000, &H35080000, &H4C070000, &H64060000, &H68060000, &H4E070000, &H36080000, &H37080000, _
     &H38080000, &H23080101, &H24080100, &HD070306, &HD070307, &H39080000, &H50070000, &H3A080000, _
     &H3B080000, &HE080200, &HE080201, &HE080202, &HE080203, &H24080101, &HF080200, &HF080201, _
     &H25080100, &HF080202, &HF080203, &H10080200, &H10080201, &H10080202, &H10080203, &H80300, _
     &H25080101, &H26080100, &H11080200, &H11080201, &H11080202, &H11080203, &H12080200, &H12080201, _
     &H12080202, &H12080203, &H13080200, &H26080101, &H13080201, &H13080202, &H13080203, &H14080200, _
     &H27080100, &H14080201, &H14080202, &H27080101, &H14080203, &H28080100, &H15080200, &H15080201, _
     &H15080202, &H15080203, &H16080200, &H16080201, &H28080101, &H29080100, &H16080202, &H80301, _
     &H29080101, &H3C080000, &H2A080100, &H16080203, &H80302, &H2A080101, &H2B080100, &H17080200, _
     &H2B080101, &H17080201, &H17080202, &H17080203, &H80303, &H18080200, &H18080201, &H18080202, _
     &H18080203, &H19080200, &H2C080100, &H2C080101, &H2D080100, &H19080201, &H19080202, &H52070000, _
     &H54070000, &H56070000, &H19080203, &H2D080101, &H3D080000, &H58070000, &H1A080200, &H1A080201, _
     &H1A080202, &H80304, &H1A080203, &H1B080200, &H1B080201, &H1B080202, &H1B080203, &H1C080200, _
     &H1C080201, &H1C080202, &H1C080203, &H80305, &H1D080200, &H1D080201, &H1D080202, &H1D080203, _
     &H5A070000, &H1E080200, &H1E080201, &H1E080202, &H80306, &H80307, &H1080300, &H1080301, _
     &H1080302, &H1E080203, &H1F080200, &H1080303, &H1080304, &H1080305, &H1080306, &H1F080201, _
     &H2E080100, &H1080307, &H2080300, &H1F080202, &H2080301, &H2E080101, &H2F080100, &H2F080101, _
     &H1F080203, &H2080302, &H2080303, &H2080304, &H2080305, &H2080306, &H2080307, &H3080300, _
     &H20080200, &H3080301, &H20080201, &H3080302, &H3080303, &H3080304, &H3080305, &H3080306, _
     &H3080307, &H4080300, &H4080301, &H4080302, &H4080303, &H4080304, &H4080305, &H4080306, _
     &H20080202, &H4080307, &H5080300, &H5080301, &H5080302, &H5080303, &H5080304, &H5080305, _
     &H20080203, &H5080306, &H5080307, &H6080300, &H6080301, &H6080302, &H6080303, &H6080304, _
     &H21080200, &H6080305, &H21080201, &H6080306, &H6080307, &H7080300, &H7080301, &H7080302, _
     &H7080303, &H7080304, &H7080305, &H7080306, &H7080307, &H8080300, &H21080202, &H8080301, _
     &H8080302, &H8080303, &H8080304, &H8080305, &H8080306, &H8080307, &H9080300, &H9080301, _
     &H9080302, &H9080303, &H9080304, &H9080305, &H9080306, &H9080307, &HA080300, &HA080301, _
     &H21080203, &HA080302, &H22080200, &HA080303, &HA080304, &HA080305, &HA080306, &HA080307, _
     &H22080201, &HB080300, &HB080301, &HB080302, &HB080303, &HB080304, &HB080305, &HB080306, _
     &H22080202, &HB080307, &HC080300, &HC080301, &HC080302, &HC080303, &HC080304, &HC080305, _
     &H22080203, &HC080306, &HC080307, &HD080300, &HD080301, &HD080302, &HD080303, &H6C060000}

#End Region

#Region " Decompression "

    Public Function ComputeDataHeaderLength(ByVal Packet() As Byte) As Integer
        If Packet(0) < &HF0 Then Return 1 Else Return 2
    End Function

    Public Function ComputeDataLength(ByVal Packet() As Byte) As Integer

        If Packet(0) < &HF0 Then
            Return Packet(0)
        End If

        Return ((Packet(0) And &HF) << 8) + Packet(1)
    End Function

    Public Function DecompressDataContent(ByVal Packet() As Byte, ByVal HeaderLength As Integer, ByVal PacketLength As Integer, ByRef DecompressedBytes() As Byte, ByVal DecompressionBufferLength As Integer) As Integer
        Dim a, b, c, d, x, z As UInt32
        Dim maxcnt, index, cnt, size As UInt32

        Dim CompressedBytes() As Byte = Packet

        b = 0
        x = HeaderLength
        z = 0

        size = CUInt(PacketLength - HeaderLength)
        maxcnt = DecompressionBufferLength
        cnt = &H20

        While (True)
            If cnt >= &H8 Then
                While (size > 0 And cnt >= 8)
                    cnt -= &H8
                    size -= 1
                    a = (CUInt(CompressedBytes(x)) << CInt(cnt))
                    x += 1
                    b = b Or a
                End While
            End If

            index = CharIndex(b >> &H18)
            a = CharTable(index)
            d = (CUInt(b >> CInt(&H18 - a))) And BitMasks(a)
            c = CharTable(index + 2 * d + 2)

            cnt += c
            If (cnt > &H20) Then
                'Dim Result As String = ""
                'For j As Integer = 0 To 24567 - maxcnt - 1
                '    Result += DecompressedBytes(j).ToString("x") & " "
                'Next
                'Log.WriteLine(Result)
                Return DecompressionBufferLength - maxcnt
            End If

            If (maxcnt - 1 = 0) Then
                Return -1
            End If
            maxcnt -= 1

            a = CharTable(index + (2 * d) + 1)
            DecompressedBytes(z) = a
            z += 1

            b <<= CInt(c And &HFF)
        End While

        Return -1
    End Function

    Public Function DecompressPacket(ByVal Packet As Byte(), ByRef DecompressedBytes() As Byte, ByVal DecompressionBufferLength As Integer) As Integer

        Dim headerLength As Integer = ComputeDataHeaderLength(Packet)
        Dim CompressedPacketLength = ComputeDataLength(Packet)

        If CompressedPacketLength = 0 Then
            Return 0
        Else
            Dim PacketLength As Integer = DecompressDataContent(Packet, headerLength, CompressedPacketLength, DecompressedBytes, DecompressionBufferLength)
            Return PacketLength
        End If

    End Function

#End Region

#Region " Compression "

    Public Function CompressPacket(ByVal Packet() As Byte, ByVal PacketLength As Integer, ByRef CompressedBytes() As Byte, ByVal CompressionBufferLength As Integer) As Integer

        Dim contentLength As Integer = CompressDataContent(Packet, PacketLength, CompressedBytes, 0, CompressionBufferLength)
        If contentLength = -1 Then Return -1

        Dim headerLength As Integer = CreateDataHeader(contentLength, CompressedBytes, CompressionBufferLength)
        If headerLength = -1 Then Return -1

        Dim dataLength As Integer = CompressDataContent(Packet, PacketLength, CompressedBytes, headerLength, CompressionBufferLength - headerLength)
        If dataLength <> -1 Then dataLength += headerLength

        Return dataLength - 1
    End Function

    Public Function CompressDataContent(ByVal Packet() As Byte, ByVal PacketLength As Integer, ByRef CompressedBytes() As Byte, ByVal StartPosition As Integer, ByVal CompressionBufferLength As Integer) As Integer
        Dim a, e, buf As UInt32
        Dim cnt As Integer = 0
        Dim x, z As Integer
        z = StartPosition
        'unsigned char* tmp = static_cast<unsigned char*>(outData);
        'const unsigned char* indata = static_cast<const unsigned char*>(inData);
        'unsigned char* outdata = static_cast<unsigned char*>(outData);

        Dim DecompressedBytes() As Byte = Packet

        While (PacketLength > 0)

            a = CompressionTable(DecompressedBytes(x))
            x += 1
            e = (a And &HFF00) >> 8

            buf = buf Or ((a >> 24) << (24 - cnt))
            cnt += (a And &HFF0000) >> 16

            If (e) Then
                buf = buf Or (((a And &HFF) << (8 - e)) << (24 - cnt))
                cnt += e
            End If

            While (cnt > 8)
                CompressedBytes(z) = buf >> 24
                z += 1

                If (z >= CompressionBufferLength) Then
                    Return -1
                End If

                cnt -= 8
                buf <<= 8
            End While
            PacketLength -= 1
        End While

        While (cnt > 0)
            CompressedBytes(z) = buf >> 24
            z += 1

            If (z >= CompressionBufferLength) Then
                Return -1
            End If

            buf <<= 8
            cnt -= 8
        End While
        Return z
    End Function

    Public Function CreateDataHeader(ByVal contentLength As Integer, ByRef bytes() As Byte, ByVal maxHeaderLength As Integer) As Integer
        Dim dataLength, headerLength As Integer

        If (contentLength > 238) Then

            headerLength = 2

            If (maxHeaderLength < headerLength) Then
                Return -1
            End If

            dataLength = contentLength + headerLength
            dataLength = dataLength Or &HF000

            bytes(0) = dataLength >> 8
            bytes(1) = dataLength And &HFF

        Else
            headerLength = 1

            If (maxHeaderLength < headerLength) Then
                Return -1
            End If

            dataLength = contentLength + headerLength
            bytes(0) = dataLength
        End If

        Return headerLength
    End Function

#End Region

#Region " Packet Splitting "

    Function SplitPackets(ByVal buffer() As Byte, ByVal length As Integer) As List(Of Byte())

        Dim offset As Integer = 0
        Dim Packets As New List(Of Byte())

        While offset < length
            Dim packetSize As Integer = GetDecompressedPacketSize(buffer, offset, length - offset)

            If (packetSize < 0) Then

                '"Error: Unexpected server packetId encountered: 0x" & buffer(offset).ToString("x") & " last packet was " & lastPacket.ToString("x"), True)

                Dim Result As String = ""
                For i As Integer = 0 To length - 1
                    Result += buffer(i).ToString("x") & " "
                Next

                Exit While
            End If

            If packetSize <= 0 Or (length - offset) < 0 Then
                '"Error: Unexpected end of server data encountered", True)
                Exit While
            End If

            ' a packet size was successfully obtained; create a packet and store the
            ' current segment of the lump inside of it

            Dim PacketBuffer(packetSize) As Byte

            Array.Copy(buffer, offset, PacketBuffer, 0, packetSize)

            Dim packet() As Byte = PacketBuffer

            Packets.Add(packet)

            offset += packetSize

        End While

        If offset = length Then
            'Success
            Return Packets
        Else
            'Failure
            '"Packet Split Failure", True)
            Return Nothing
        End If

    End Function


    Dim lastPacket As Integer

    Function GetDecompressedPacketSize(ByVal bytes() As Byte, ByVal offset As Integer, ByVal size As Integer) As Integer
        Dim packetId As Integer = bytes(offset)
        Select Case packetId
            Case &H26
                lastPacket = packetId
                Return GetChatPacketSize(bytes, offset, size)
            Case &H5B
                lastPacket = packetId
                'Return bytes(offset + 1)
                Return BitConverter.ToUInt16(bytes, (offset + 1))
            Case &H94
                If (size >= 2) Then
                    lastPacket = packetId
                    Return (6 + (bytes((offset + 1)) * 3))
                End If
            Case &HA8, &HAA
                If (size >= 7) Then
                    lastPacket = packetId
                    Return bytes(offset + 6)
                End If
            Case &HAC
                If (size >= 13) Then
                    lastPacket = packetId
                    Return bytes(offset + 12)
                End If
            Case &HAE
                If (size >= 3) Then
                    lastPacket = packetId
                    'Return 3 + bytes(offset + 1)
                    Return (3 + BitConverter.ToUInt16(bytes, (offset + 1)))
                End If
            Case &H9C, &H9D
                If (size >= 3) Then
                    lastPacket = packetId
                    Return bytes(offset + 2)
                End If
            Case Else
                If packetId < PacketSizes.Length Then
                    lastPacket = packetId
                    Return PacketSizes(packetId)
                End If
        End Select

        Return -1
    End Function

    Function GetChatPacketSize(ByVal bytes() As Byte, ByVal index As Integer, ByVal size As Integer) As Integer
        Dim utf As New System.Text.UTF7Encoding
        Dim Name As String = ""
        Dim Message As String = ""
        Dim ni As Integer = index + 10
        While bytes(ni) <> &H0
            Name += utf.GetString(bytes, ni, 1)
            ni += 1
        End While
        ni += 1
        While bytes(ni) <> &H0
            Message += utf.GetString(bytes, ni, 1)
            ni += 1
        End While
        Return 10 + Name.Length + 1 + Message.Length + 1
    End Function

#End Region

End Module
